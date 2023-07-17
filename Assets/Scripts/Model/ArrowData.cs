using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI weightText;

    public void SetWeight(string weight) {
        weightText.text = weight;
    }
}
