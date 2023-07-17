using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pickUpPointText;

    public void SetPickUp(string coordinate) {
        pickUpPointText.text = coordinate;
    }
}
