using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SetUIText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textField;
    [SerializeField]
    private String fixedText;

    public void OnSliderValueChanged(float numericValue){
        textField.text = $"{fixedText}: {numericValue}";
    }
}
