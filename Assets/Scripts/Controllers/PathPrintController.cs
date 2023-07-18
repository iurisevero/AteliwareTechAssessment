using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathPrintController : MonoBehaviour
{
    const string NodeObjectPoolKey = "PathPrintController.NodeObject";
    const string ArrowObjectPoolKey = "PathPrintController.ArrowObject";

    [SerializeField] GameObject startPrefab;
    [SerializeField] GameObject pickUpPrefab;
    [SerializeField] GameObject endPrefab;
    [SerializeField] GameObject nodePrefab;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] RectTransform pathContainer;

    private void Start()
    {
        GameObjectPoolController.AddEntry(NodeObjectPoolKey, nodePrefab, 5, 64);
        GameObjectPoolController.AddEntry(ArrowObjectPoolKey, arrowPrefab, 5, 64);
    }

    private GameObject Dequeue(string PoolKey){
        Poolable poolObj = GameObjectPoolController.Dequeue(PoolKey);
        return poolObj.gameObject;
    }

    private GameObject DequeueNode(string node){
        GameObject nodeObj = Dequeue(NodeObjectPoolKey);
        NodeData nodeData = nodeObj.GetComponent<NodeData>();
        nodeData.SetNode(node);
        return nodeObj;
    }

    private GameObject DequeueArrow(string weight){
        Poolable arrowObj = GameObjectPoolController.Dequeue(ArrowObjectPoolKey);
        ArrowData arrowData = arrowObj.GetComponent<ArrowData>();
        arrowData.SetWeight(weight);
        return arrowObj.gameObject;
    }

    private void Enqueue(GameObject portraitObject){
        Poolable p = portraitObject.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }

    private void SetObjParent(GameObject nodeObj) {
        nodeObj.transform.localScale = Vector3.one;
        nodeObj.gameObject.SetActive(true);
        nodeObj.transform.SetParent(pathContainer, false);
    }

    private void InstantiateStartNode(string node) {
        GameObject startPointObj = Instantiate(
            startPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0)
        );
        SetObjParent(startPointObj);
        StartData startData = startPointObj.GetComponent<StartData>();
        startData.SetNode(node);
    }

    private void InstantiatePickUpNode(string node) {
        GameObject pickUpPointObj = Instantiate(
            pickUpPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0)
        );
        SetObjParent(pickUpPointObj);
        PickUpData pickUpData = pickUpPointObj.GetComponent<PickUpData>();
        pickUpData.SetNode(node);
    }

    private void InstantiateEndNode(string node) {
        GameObject endPointObj = Instantiate(
            endPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0)
        );
        SetObjParent(endPointObj);
        EndData endData = endPointObj.GetComponent<EndData>();
        endData.SetNode(node);
    }

    public void PrintPath(string pickUp, string destiny, List<(string, string)> path) {
        Clear();

        InstantiateStartNode(path[0].Item1);
        GameObject arrowObj;
        for(int i=1; i < path.Count; ++i) {
            arrowObj = DequeueArrow("");
            SetObjParent(arrowObj);
            

            if(path[i].Item1 == pickUp) {
                InstantiatePickUpNode(path[i].Item1);
            } else {
                GameObject nodeObj = DequeueNode(path[i].Item1);
                SetObjParent(nodeObj);
            }
        }

        arrowObj = DequeueArrow("");
        SetObjParent(arrowObj);
        InstantiateEndNode(destiny);
    }

    public void Clear() {
        int count = 0;
        while(pathContainer.childCount != 0 && count < 100){
            Transform childTransform = pathContainer.GetChild(0);
            if(childTransform.GetComponent<StartData>() ||
                childTransform.GetComponent<PickUpData>() ||
                childTransform.GetComponent<EndData>()
            ){
                DestroyImmediate(childTransform.gameObject);
            } else {
                Enqueue(childTransform.gameObject);
            }
            count++;
        }
    }

}
