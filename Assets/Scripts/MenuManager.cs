using System.Collections;
using System.Collections.Generic;
using TS.ColorPicker;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static ShapeGenerator;

public class MenuManager : MonoBehaviour
{
    private ShapeGenerator shapeGenerator;
    [SerializeField] private ColorPicker _colorPicker;
    public ShapeGenerator.Shape shape;

    [Header("Shape Color")]
    [SerializeField] Color color = Color.white;

    [Header("Cube Settings")]

    [SerializeField]
    private float cubeA; [SerializeField] float cubeB; [SerializeField] float cubeH; //cube properties

    [Header("Sphere Settings")]

    [SerializeField]
    private float sphereRad;
    [SerializeField] int sphereRes; //sphere properties

    [Header("Prism Settings")]

    [SerializeField]
    private float prismRad; [SerializeField] float prismH; [SerializeField] int prismFaces; //prism properties

    [Header("Capsule Settings")]

    [SerializeField]
    private float capsuleRad; [SerializeField] float capsuleH; [SerializeField] int capsuleRes; //capsule properties

    [Header("UI Elements")]
    public Image colorImage;
    public GameObject dropdownObject;
    private TMP_Dropdown dropdown;

    public GameObject cubePanel;
    public GameObject spherePanel;
    public GameObject prismPanel;
    public GameObject capsulePanel;

    public Button createButton;
    public Button deleteButton;


    void Start()
    {
        dropdown = dropdownObject.GetComponent<TMP_Dropdown>();
        shapeGenerator = GameObject.Find("ShapeGenerator").GetComponent<ShapeGenerator>();
        _colorPicker.OnSubmit.AddListener(ColorPicker_OnSubmit);
    }

    public void CreateShape()
    {
        SetData();
        shapeGenerator.GenerateShape();
        CheckDeleteAvailability();
    }
    public void DeleteShape()
    {
        shapeGenerator.SetShape(shape);
        shapeGenerator.DeleteShape();
        CheckDeleteAvailability();
    }

    public void ÑhooseColor()
    {
        _colorPicker.Open(color);
    }
    public void DropdownChanged()
    {
        DisableParametersPanel();
        switch (dropdown.value)
        {
            case 0:
                shape = ShapeGenerator.Shape.Cube;
                cubePanel.SetActive(true);
                break;
            case 1:
                shape = ShapeGenerator.Shape.Prism;
                prismPanel.SetActive(true);
                break;
            case 2:
                shape = ShapeGenerator.Shape.Sphere;
                spherePanel.SetActive(true);
                break;
            case 3:
                shape = ShapeGenerator.Shape.Capsule;
                capsulePanel.SetActive(true);
                break;
        }
        CheckDeleteAvailability();
    }
    public void ColorPicker_OnSubmit(Color color)
    {
        this.color = color;
        colorImage.color = color;
    }
    private void SetData()
    {
        SetDataCube();
        SetDataPrism();
        SetDataSphere();
        SetDataCapsule();
        shapeGenerator.SetColor(color);
        shapeGenerator.SetShape(shape);
    }
    private void SetDataCube()
    {
        CubeInputData cubeInputData = cubePanel.GetComponent<CubeInputData>();

        cubeA = cubeInputData.inputA;
        cubeB = cubeInputData.inputB;
        cubeH = cubeInputData.inputH;
        shapeGenerator.SetDataCube(cubeA, cubeB, cubeH);

    }
    private void SetDataPrism()
    {
        PrismInputData prismInputData = prismPanel.GetComponent<PrismInputData>();

        prismRad = prismInputData.inputRad;
        prismH = prismInputData.inputH;
        prismFaces = prismInputData.inputFaces;
        shapeGenerator.SetDataPrism(prismRad, prismH, prismFaces);
    }
    private void SetDataSphere()
    {
        SphereInputData sphereInputData = spherePanel.GetComponent<SphereInputData>();

        sphereRad = sphereInputData.inputRad;
        sphereRes = sphereInputData.inputRes;
        shapeGenerator.SetDataSphere(sphereRad, sphereRes);
    }
    private void SetDataCapsule()
    {
        CapsuleInputData capsuleInputData = capsulePanel.GetComponent<CapsuleInputData>();

        capsuleRad = capsuleInputData.inputRad;
        capsuleH = capsuleInputData.inputH;
        capsuleRes = capsuleInputData.inputRes;
        shapeGenerator.SetDataCapsule(capsuleRad, capsuleH,capsuleRes);
    }

    private void DisableParametersPanel()
    {
        cubePanel.SetActive(false);
        prismPanel.SetActive(false);
        spherePanel.SetActive(false);
        capsulePanel.SetActive(false);
    }
    private void CheckDeleteAvailability()
    {
        switch (shape)
        {
            case Shape.Cube:
                if (shapeGenerator.cubeExists)
                    AllowDelete();
                else
                    BlockDelete();
                break;
            case Shape.Sphere:
                if (shapeGenerator.sphereExists)
                    AllowDelete();
                else
                    BlockDelete();
                break;
            case Shape.Capsule:
                if (shapeGenerator.capsuleExists)
                    AllowDelete();
                else
                    BlockDelete();
                break;
            case Shape.Prism:
                if (shapeGenerator.prismExists)
                    AllowDelete();
                else
                    BlockDelete();
                break;
        }
    }
    public void BlockCreation()
    {
        createButton.interactable = false;
    }
    private void BlockDelete()
    {
        deleteButton.interactable = false;
    }
    public void AllowCreation()
    {
        createButton.interactable = true;
    }
    private void AllowDelete()
    {
        deleteButton.interactable = true;
    }
}
