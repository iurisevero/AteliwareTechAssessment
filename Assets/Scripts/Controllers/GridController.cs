using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    const float gridSize = 122f;
    [SerializeField] GameObject drone;
    [SerializeField] GameObject package;
    [SerializeField] GameObject destination;
    private static readonly Color32 droneColor = new Color32(0, 108, 0, 255);
    private static readonly Color32 packageColor = new Color32(255, 255, 83, 255);
    private static readonly Color32 destinationColor = new Color32(192, 0, 0, 255);
    private Coroutine pathTraversalRoutine;

    // void Start()
    // {
    //     List<(string, string)> path = new List<(string, string)>(){
    //         ("A1", "B1"),
    //         ("B1", "C1"),
    //         ("C1", "C2"),
    //         ("C2", "C3"),
    //         ("C3", "D3"),
    //         ("D3", "D4"),
    //         ("D4", "D5"),
    //         ("D5", "E5"),
    //         ("E5", "E6"),
    //         ("E6", "F6"),
    //         ("F6", "G6"),
    //         ("G6", "G7"),
    //         ("G7", "H7"),
    //         ("H7", "H8")
    //     };
    //     string startPoint = "A1", pickUpPoint = "D4", endPoint = "H8";
    //     TraversePath(
    //         startPoint, pickUpPoint, endPoint, path
    //     );
    // }

    private IEnumerator TraversePathRoutine(
        string pickUpPoint,
        List<(string, string)> path
    ) {
        foreach(var nodes in path) {
            Vector2 direction = Utilities.GetDirectionFromNodes(
                nodes.Item1, nodes.Item2
            );
            yield return StartCoroutine(Move(direction, 1f));
            
            if(nodes.Item2 == pickUpPoint){
                package.SetActive(false);
                drone.GetComponentInChildren<Image>().color = packageColor;
            }
        }

        drone.SetActive(false);
        destination.GetComponentInChildren<Image>().color = packageColor;
    }

    private IEnumerator Move(Vector2 direction, float moveDuration) {
        Vector2 startPosition = drone.transform.localPosition;
        Vector2 endPosition = startPosition + (direction * gridSize);

        float elapsedTime = 0;
        while(elapsedTime < moveDuration) {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            drone.transform.localPosition = Vector2.Lerp(
                startPosition, endPosition, percent
            );
            yield return null;
        }

        drone.transform.localPosition = endPosition;        
    }

    private void SetInitialPosition(
        string startPoint, string pickUpPoint, string endPoint
    ) {
        Vector2 startPos = Utilities.ChessboardToMatriz(startPoint);
        drone.gameObject.SetActive(true);
        drone.GetComponent<RectTransform>().anchoredPosition = startPos * gridSize;
        drone.GetComponentInChildren<Image>().color = droneColor;

        Vector2 pickUpPos = Utilities.ChessboardToMatriz(pickUpPoint);
        package.gameObject.SetActive(true);
        package.GetComponent<RectTransform>().anchoredPosition = pickUpPos * gridSize;
        package.GetComponentInChildren<Image>().color = packageColor;

        Vector2 endPos = Utilities.ChessboardToMatriz(endPoint);
        destination.gameObject.SetActive(true);
        destination.GetComponent<RectTransform>().anchoredPosition = endPos * gridSize;
        destination.GetComponentInChildren<Image>().color = destinationColor;
    }

    public void TraversePath(
        string startPoint, string pickUpPoint, string endPoint,
        List<(string, string)> path
    ) {
        SetInitialPosition(startPoint, pickUpPoint, endPoint);
        pathTraversalRoutine = StartCoroutine(TraversePathRoutine(pickUpPoint, path));
    }

    public void SkipAnimation() {
        StopCoroutine(pathTraversalRoutine);
    }
}
