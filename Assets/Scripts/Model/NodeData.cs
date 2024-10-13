using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nodePointText;

    public void SetNode(string coordinate) {
        nodePointText.text = coordinate;
    }
}
