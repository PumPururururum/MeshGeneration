using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SphereInputData : MonoBehaviour
{
    public float inputRad;
    public int inputRes;
    public TMP_InputField inputFRad;
    public TMP_InputField inputFRes;

    public MenuManager menuManager;

    public void ReadInputFieldRad()
    {
        inputRad = float.Parse(inputFRad.text);
        CheckInput(inputRad);
    }
    public void ReadInputFieldRes()
    {
        inputRes = int.Parse(inputFRes.text);
        CheckInput(inputRes);
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
