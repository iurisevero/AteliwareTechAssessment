using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI startPointText;

    public void SetStart(string coordinate) {
        startPointText.text = coordinate;
    }
}
