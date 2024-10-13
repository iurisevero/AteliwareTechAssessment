using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LastDeliveriesController : MonoBehaviour
{
    const int deliveriesListSize = 10;
    public LinkedList<(string, string, string, float)> deliveries;

    private void Start() {
        deliveries = new LinkedList<(string, string, string, float)>();
        for(int i=1; i <= deliveriesListSize; ++i) {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void UpdateDeliveryList() {
        int _size = deliveries.Count;

        int count = 1;
        foreach(var delivery in deliveries) {
            GameObject deliveryObj = this.transform.GetChild(count).gameObject;
            deliveryObj.SetActive(true);
            TextMeshProUGUI textMesh = deliveryObj.GetComponent<TextMeshProUGUI>();    
            string textToPrint = string.Format(
                "From {0}, picking-up at {1} to {2} in {3:F2} seconds",
                delivery.Item1, delivery.Item2, delivery.Item3, delivery.Item4
            );
            textMesh.text = textToPrint;
            count++;
        }

        for(int i=_size+1; i <= deliveriesListSize; ++i) {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void AddDeliveryToList(
        string source, string pickUp, string destiny, float time
    ) {
        deliveries.AddFirst((source, pickUp, destiny, time));
        if(deliveries.Count > deliveriesListSize)
            deliveries.RemoveLast();
        UpdateDeliveryList();
    }
}
