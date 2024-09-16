using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrismInputData : MonoBehaviour
{
    public float inputRad;
    public float inputH;
    public int inputFaces;
    public TMP_InputField inputFRad;
    public TMP_InputField inputFH;
    public TMP_InputField inputFFaces;

    public MenuManager menuManager;

    public void ReadInputFieldRad()
    {
        inputRad = float.Parse(inputFRad.text);
        CheckInput(inputRad);
    }
    public void ReadInputFieldH()
    {
        inputH = float.Parse(inputFH.text);
        CheckInput(inputH);
    }
    public void ReadInputFieldFaces()
    {
        inputFaces = int.Parse(inputFFaces.text);
        CheckInput(inputFaces);
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
    private void CheckInput(int input)
    {
        if (input < 3)
        {
            Debug.Log("Wrong parameteres");
            menuManager.BlockCreation();
        }
        else
            menuManager.AllowCreation();
    }
}
