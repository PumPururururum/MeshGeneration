using System.Collections;
using System.Collections.Generic;
using TS.ColorPicker;
using UnityEngine;
using TMPro;

public class CubeInputData : MonoBehaviour
{
    public float inputA;
    public float inputB;
    public float inputH;
    public TMP_InputField inputFA;
    public TMP_InputField inputFB;
    public TMP_InputField inputFH;

    public MenuManager menuManager;
    public void ReadInputFieldA()
    {
        inputA = float.Parse(inputFA.text);
        CheckInput(inputA);
    }
    public void ReadInputFieldB()
    {
        inputB= float.Parse(inputFB.text);
        CheckInput(inputB);
    }
    public void ReadInputFieldH()
    {
        inputH = float.Parse(inputFH.text);
        CheckInput(inputH);
    }
    private void CheckInput(float input)
    {
        if (input <= 0)
        {
            Debug.Log("Wrong parameteres");
            menuManager.BlockCreation();
        }
        else
        {
            menuManager.AllowCreation();
        }
    }
}
