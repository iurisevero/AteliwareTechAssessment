using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endPointText;

    public void SetEndPoint(string coordinate) {
        endPointText.text = coordinate;
    }
}
