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
    private static readonly string API_URL = "https://mocki.io/v1/10404696-fd43-4481-a7ed-f9369073252f";
    [SerializeField] UserInputData startPoint;
    [SerializeField] UserInputData pickUpPoint;
    [SerializeField] UserInputData endPoint; 
    [SerializeField] Button getRoute;
    // [SerializeField] LastDeliveriesController lastDeliveriesController;
    // [SerializeField] PathPrintController pathPrintController;

    public void Update() {
        getRoute.interactable = (
            Utilities.CheckChessboardCoordinate(startPoint.GetInputValue()) &&
            Utilities.CheckChessboardCoordinate(pickUpPoint.GetInputValue()) &&
            Utilities.CheckChessboardCoordinate(endPoint.GetInputValue())
        );
    }

    public void GetRoute() {
        WeightedGraph<string, ReverseFloat> board;
        Observable.FromCoroutine<string>(observer =>
        {
            return SendRequest(observer, API_URL);
        }).Subscribe(response =>
        {
            Debug.Log($"Resposta da requisição GET: {response}");
            board = HandleResponse(response);

            string debug = "";
            foreach(var node in board.nodes) {
                debug += node.Key + ":";
                foreach(var neighbor in node.Value) {
                    debug += " (" + neighbor.Item1 + ", " + neighbor.Item2.Value + ")";
                }
                debug+= "\n";
            }
            Debug.Log("Board: " + debug);

            float start = board.SSSPDijkstra(startPoint.GetInputValue(), pickUpPoint.GetInputValue(), 0, -1).Value;
            float mid = board.SSSPDijkstra(pickUpPoint.GetInputValue(), endPoint.GetInputValue(), 0, -1).Value;
            Debug.Log($"Dijkstra from {startPoint.GetInputValue()} to {pickUpPoint.GetInputValue()}: {start}");
            Debug.Log($"Dijkstra from {pickUpPoint.GetInputValue()} to {endPoint.GetInputValue()}: {mid}");
            Debug.Log($"Full path: {start + mid}");
        }, error =>
        {
            Debug.LogError($"Erro na requisição GET: {error.Message}");
        });
    }

    public WeightedGraph<string, ReverseFloat> HandleResponse(string response) {
        WeightedGraph<string, ReverseFloat> board = 
            new WeightedGraph<string, ReverseFloat>(float.MaxValue);
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
