using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInputData : MonoBehaviour
{
    [SerializeField] Sprite right;
    [SerializeField] Sprite wrong;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Image checkImage;

    public void SelectInputField() {
        inputField.Select();
    }

    public string GetInputValue() {
        return inputField.text;
    }

    public void CheckUserInput(string value) {
        checkImage.gameObject.SetActive(!(value == ""));

        if(Utilities.CheckChessboardCoordinate(value)) {
            checkImage.sprite = right;
        } else {
            checkImage.sprite = wrong;
        }
    }
}
