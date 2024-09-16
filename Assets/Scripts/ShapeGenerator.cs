using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    
    private Shape shape = new Shape();
    private Color color;
    public enum Shape
    {
        Cube, Prism, Sphere, Capsule
    }
    public bool cubeExists, prismExists, sphereExists, capsuleExists;


    private float cubeA;  float cubeB;  float cubeH; //cube properties
    public GameObject cubeGenerator;

    private float sphereRad; int sphereRes; //sphere properties
    public GameObject sphereGenerator;

    private float prismRad;  float prismH; int prismFaces; //prism properties
    public GameObject prismGenerator;

    private float capsuleRad; float  capsuleH; int capsuleRes; //capsule properties
    public GameObject capsuleGenerator;

    public void GenerateShape()
    {
        switch (shape)
        {
            case Shape.Cube:
                GenerateCube();
                break;
            case Shape.Sphere:
                GenerateSphere();
                break;
            case Shape.Capsule:
                GenerateCapsule();
                break;
            case Shape.Prism:
                GeneratePrism();
                break;
        }
    }
    public void DeleteShape()
    {
        switch (shape)
        {
            case Shape.Cube:
                DeleteCube();
                break;
            case Shape.Sphere:
                DeleteSphere();
                break;
            case Shape.Capsule:
                DeleteCapsule();
                break;
            case Shape.Prism:
                DeletePrism();
                break;
        }
    }
    private void GenerateCube()
    {
        cubeGenerator.SetActive(true);
        cubeGenerator.GetComponent<CubeGenerator>().Generator(cubeA, cubeH, cubeB,color);

        cubeGenerator.transform.position = new Vector3(cubeGenerator.transform.position.x,cubeH * 0.5f,cubeGenerator.transform.position.z);

        cubeExists = true;
    }
    private void GeneratePrism()
    {
        prismGenerator.SetActive(true);
        prismGenerator.GetComponent<PrismGenerator>().Generator(prismFaces, prismH, prismRad, color);
        prismExists = true;
    }
    private void GenerateSphere()
    {
        sphereGenerator.SetActive(true);
        sphereGenerator.GetComponent<SphereGenerator>().Generator(sphereRes, sphereRad, color);
        sphereGenerator.transform.position = new Vector3(-3.15f, sphereRad, 7.15f);
        sphereExists = true;
    }
    private void GenerateCapsule()
    {
        capsuleGenerator.SetActive(true);
        capsuleGenerator.GetComponent<CapsuleGenerator>().Generator(capsuleRad, capsuleH, capsuleRes, color);
        capsuleGenerator.transform.position = new Vector3(capsuleGenerator.transform.position.x, capsuleH * 0.5f, capsuleGenerator.transform.position.z);
        capsuleExists = true;
    }
    private void DeleteCube()
    {
        cubeGenerator.GetComponent<CubeGenerator>().ClearMesh();
        cubeGenerator.SetActive(false);
        cubeExists = false;

    }
    private void DeletePrism()
    {
        prismGenerator.GetComponent<PrismGenerator>().ClearMesh();
        prismGenerator.SetActive(false);
        prismExists = false;

    }
    private void DeleteSphere()
    {
        sphereGenerator.GetComponent<SphereGenerator>().ClearMesh();
        sphereGenerator.SetActive(false);
        sphereExists = false;

    }
    private void DeleteCapsule()
    {
        capsuleGenerator.GetComponent<CapsuleGenerator>().ClearMesh();
        capsuleGenerator.SetActive(false);
        capsuleExists = false;
    }

    public void SetDataCube(float cubeA, float cubeB, float cubeH)
    {
        this.cubeA = cubeA;
        this.cubeB = cubeB;
        this.cubeH = cubeH;
    }
    public void SetDataPrism(float prismRad, float prismH, int prismFaces)
    {
        this.prismFaces = prismFaces;
        this.prismRad = prismRad;
        this.prismH = prismH;
    }
    public void SetDataSphere(float sphereRad, int sphereRes)
    {
        this.sphereRad = sphereRad;
        this.sphereRes = sphereRes;
    }
    public void SetDataCapsule(float capsuleRad, float capsuleH, int capsuleRes)
    {
        this.capsuleRad = capsuleRad;
        this.capsuleH = capsuleH;
        this.capsuleRes = capsuleRes;
    }
    public void SetColor(Color color)
    {
        this.color = color;
    }
    public void SetShape(Shape shape)
    {
        this.shape = shape;
    }
}
