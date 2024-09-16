using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGenerator : MonoBehaviour
{
    private int resolution;

    private float radious;
    private Color color;

    [SerializeField,HideInInspector]
    MeshFilter[] meshFilters;
    SphereFace[] sphereFaces;
   

    public void Generator (int resolution, float radious, Color color)
    {
        this.resolution = resolution;
        this.radious = radious;
        this.color = color;
        GenerateSphere(); 
    }
    public void ClearMesh()
    {
        for (int i = 0; i < meshFilters.Length; i++)
        {
            meshFilters[i].sharedMesh.Clear();
            Destroy(meshFilters[i].gameObject);
        }
        transform.position = new Vector3(0f, 0f, 0f);
    }

    private void Initialize()
    {
        if (meshFilters == null || meshFilters.Length == 0)
            meshFilters = new MeshFilter[6];

        sphereFaces = new SphereFace[6];


        Vector3[] directions = {Vector3.up, Vector3.forward, Vector3.down, Vector3.back, Vector3.right, Vector3.left};

        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i] == null)
            {

                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();

            }
            sphereFaces[i] = new SphereFace(meshFilters[i].sharedMesh, resolution, radious, directions[i]);

        }
    }
    private void GenerateSphere()
    {
        Initialize();
        GenerateMesh();
        GenerateColor();
    }
    private void GenerateMesh()
    {
        foreach (SphereFace face in sphereFaces)

        { 
            face.ConstructMesh();
        }
    }
    

    private void GenerateColor()
    {
        foreach (MeshFilter m in meshFilters)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = color;
        }
    }
}
