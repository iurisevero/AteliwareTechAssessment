using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class UserInputController : MonoBehaviour
{
    const string API_URL = "https://mocki.io/v1/10404696-fd43-4481-a7ed-f9369073252f";
    [SerializeField] UserInputData startPoint;
    [SerializeField] UserInputData pickUpPoint;
    [SerializeField] UserInputData endPoint; 
    [SerializeField] Button getRoute;
    [SerializeField] LastDeliveriesController lastDeliveriesController;
    [SerializeField] PathPrintController pathPrintController;
    [SerializeField] GridController gridController;

    public void Update() {
        getRoute.interactable = (
            Utilities.CheckChessboardCoordinate(startPoint.GetInputValue()) &&
            Utilities.CheckChessboardCoordinate(pickUpPoint.GetInputValue()) &&
            Utilities.CheckChessboardCoordinate(endPoint.GetInputValue())
        );
    }

    public void GetRoute() {
        WeightedGraph<string, AddableFloat> board;
        Observable.FromCoroutine<string>(observer =>
        {
            return SendRequest(observer, API_URL);
        }).Subscribe(response =>
        {
            Debug.Log($"Resposta da requisição GET: {response}");
            board = HandleResponse(response);

            float start = board.SSSPDijkstra(startPoint.GetInputValue(), pickUpPoint.GetInputValue(), 0, -1).Value;
            Debug.Log($"Dijkstra from {startPoint.GetInputValue()} to {pickUpPoint.GetInputValue()}: {start}");
            
            var path = board.GetSSPDPath(startPoint.GetInputValue(), pickUpPoint.GetInputValue());

            float mid = board.SSSPDijkstra(pickUpPoint.GetInputValue(), endPoint.GetInputValue(), 0, -1).Value;
            Debug.Log($"Dijkstra from {pickUpPoint.GetInputValue()} to {endPoint.GetInputValue()}: {mid}");
    
            var path2 = board.GetSSPDPath(pickUpPoint.GetInputValue(), endPoint.GetInputValue());

            path.AddRange(path2);
            string debug = "Path: ";
            foreach(var nodes in path) {
                debug += "(" + nodes.Item1 + ", " + nodes.Item2 + ") ";
            }
            Debug.Log(debug);

            lastDeliveriesController.AddDeliveryToList(
                startPoint.GetInputValue(), 
                pickUpPoint.GetInputValue(),
                endPoint.GetInputValue(),
                start + mid
            );

            pathPrintController.PrintPath(
                pickUpPoint.GetInputValue(),
                endPoint.GetInputValue(),
                path
            );

            gridController.TraversePath(
                startPoint.GetInputValue(), 
                pickUpPoint.GetInputValue(),
                endPoint.GetInputValue(),
                path
            );
        }, error =>
        {
            Debug.LogError($"Erro na requisição GET: {error.Message}");
        });
    }

    public WeightedGraph<string, AddableFloat> HandleResponse(string response) {
        WeightedGraph<string, AddableFloat> board = 
            new WeightedGraph<string, AddableFloat>(float.MaxValue);
        Dictionary<string, Dictionary<string, float>> nodes = 
                JsonConvert.DeserializeObject<
                    Dictionary<string, Dictionary<string, float>>
                >(response);
            
        foreach(var node in nodes) {
            board.AddNode(node.Key);
            foreach(var neighbors in node.Value){
                board.AddNode(node.Key, neighbors.Key, neighbors.Value);
            }
        }
        return board;
    }

    private IEnumerator SendRequest(IObserver<string> observer, string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                observer.OnNext(www.downloadHandler.text);
                observer.OnCompleted();
            }
            else
            {
                observer.OnError(new Exception(www.error));
            }
        }
    }
}
