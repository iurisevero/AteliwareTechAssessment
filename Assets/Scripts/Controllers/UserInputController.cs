using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using UnityEngine.EventSystems;

public class UserInputController : MonoBehaviour
{
    const string API_URL = "https://mocki.io/v1/10404696-fd43-4481-a7ed-f9369073252f";
    const int fieldsCount = 3;
    [SerializeField] UserInputData startPoint;
    [SerializeField] UserInputData pickUpPoint;
    [SerializeField] UserInputData endPoint; 
    [SerializeField] Button getRoute;
    [SerializeField] Toggle animationToggle;
    [SerializeField] LastDeliveriesController lastDeliveriesController;
    [SerializeField] PathPrintController pathPrintController;
    [SerializeField] GridController gridController;
    private int fieldsIndex = -1;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log($"Input Get Down fieldIndex: {fieldsIndex}");
            fieldsIndex = (fieldsIndex + 1) % fieldsCount;
            switch (fieldsIndex)
            {
                case 1:
                    pickUpPoint.SelectInputField();
                    break;
                case 2:
                    endPoint.SelectInputField();
                    break;
                default:
                    startPoint.SelectInputField();
                    break;
            }
        }

        if(Utilities.CheckChessboardCoordinate(startPoint.GetInputValue()) &&
            Utilities.CheckChessboardCoordinate(pickUpPoint.GetInputValue()) &&
            Utilities.CheckChessboardCoordinate(endPoint.GetInputValue()) &&
            !gridController.playingAnimation
        ) {
            getRoute.interactable = (true);
            if (Input.GetKeyDown(KeyCode.Return)) {
                GetRoute();
            }
        } else {
            getRoute.interactable = (false);
        }
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

    public void OnSelectInputField(int index) {
        fieldsIndex = index;
    }

    public void GetRoute() {
        EventSystem.current.SetSelectedGameObject(null);
        WeightedGraph<string, AddableFloat> board;
        Observable.FromCoroutine<string>(observer =>
        {
            return SendRequest(observer, API_URL);
        }).Subscribe(response =>
        {
            Debug.Log($"Resposta da requisição GET: {response}");
            board = HandleResponse(response);

            float timeToGetPackage = board.SSSPDijkstra(startPoint.GetInputValue(), pickUpPoint.GetInputValue(), 0, -1).Value;
            Debug.Log($"Dijkstra from {startPoint.GetInputValue()} to {pickUpPoint.GetInputValue()}: {timeToGetPackage}");
            
            var pathToPackage = board.GetSSPDPath(startPoint.GetInputValue(), pickUpPoint.GetInputValue());

            float timeToGetDestination = board.SSSPDijkstra(pickUpPoint.GetInputValue(), endPoint.GetInputValue(), 0, -1).Value;
            Debug.Log($"Dijkstra from {pickUpPoint.GetInputValue()} to {endPoint.GetInputValue()}: {timeToGetDestination}");
    
            var pathToDestination = board.GetSSPDPath(pickUpPoint.GetInputValue(), endPoint.GetInputValue());

            pathToPackage.AddRange(pathToDestination);
    
            lastDeliveriesController.AddDeliveryToList(
                startPoint.GetInputValue(), 
                pickUpPoint.GetInputValue(),
                endPoint.GetInputValue(),
                timeToGetPackage + timeToGetDestination
            );

            pathPrintController.PrintPath(
                pickUpPoint.GetInputValue(),
                endPoint.GetInputValue(),
                pathToPackage
            );

            if(animationToggle.isOn) {
                gridController.TraversePath(
                    startPoint.GetInputValue(), 
                    pickUpPoint.GetInputValue(),
                    endPoint.GetInputValue(),
                    pathToPackage
                );
            }
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
}
