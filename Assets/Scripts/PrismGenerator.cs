using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PrismGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    private float radious, height;
    private int numOfFaces;
    private Color color;

    public void Generator(int numOfFaces, float height, float radious, Color color)
    {
        this.numOfFaces = numOfFaces;
        this.height = height;
        this.radious = radious;
        this.color = color;
        Initialize();
        GeneratePrism();
        UpdateMesh();
    }
    public void ClearMesh()
    {
        mesh.Clear();
    }

    private void Initialize()
    {
        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        mesh = mf.sharedMesh;
        if (mesh == null || GetComponent<MeshFilter>() == null)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

        }
    }
    private void GenerateColor()
    {
        GetComponent<MeshRenderer>().sharedMaterial.color = color;
    }
    public void GeneratePrism()
    {
        GenerateVertices();
        GenerateTriangles();   
        GenerateColor();
    }
    private void GenerateVertices()
    {
        vertices = new Vector3[numOfFaces * 2 + 4 * numOfFaces];
        float edgeLength = 2 * radious * Mathf.Sin(Mathf.PI / numOfFaces);
        float angle = 2 * Mathf.PI / numOfFaces;
        int j = 0;
        for (int i = 0; i < numOfFaces; i++)
        {
            vertices[i] = new Vector3(Mathf.Sin(i * angle) * radious, 0, Mathf.Cos(i * angle) * radious); //base polygon
            vertices[j + numOfFaces * 2] = new Vector3(Mathf.Sin(i * angle) * radious, 0, Mathf.Cos(i * angle) * radious);
            vertices[j + numOfFaces * 2 + 1] = new Vector3(Mathf.Sin(i * angle) * radious, 0, Mathf.Cos(i * angle) * radious);//vertices for side triangles
            j += 2;
        }
        j = numOfFaces * 2;
        for (int i = numOfFaces; i < numOfFaces * 2; i++)
        {
            vertices[i] = new Vector3(Mathf.Sin(i * angle) * radious, height, Mathf.Cos(i * angle) * radious); // upper polygon
            vertices[j + numOfFaces * 2] = new Vector3(Mathf.Sin(i * angle) * radious, height, Mathf.Cos(i * angle) * radious);
            vertices[j + numOfFaces * 2 + 1] = new Vector3(Mathf.Sin(i * angle) * radious, height, Mathf.Cos(i * angle) * radious);//vertices for side triangles
            j += 2;
        }
    }
    private void GenerateTriangles()
    {
        triangles = new int[3 * (numOfFaces - 2) * 2 + numOfFaces * 6];
        for (int i = 0; i < numOfFaces - 2; i++) //base triangles
        {
            triangles[3 * i] = i + 2;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = 0;
        }

        for (int i = numOfFaces - 2; i < (numOfFaces - 2) * 2; i++) //upper triangles
        {

            triangles[3 * i] = numOfFaces;
            triangles[3 * i + 1] = i + 1 + 2;
            triangles[3 * i + 2] = i + 2 + 2;
        }

        //side triangles
        int j = numOfFaces * 2 + 1;

        for (int i = (numOfFaces - 2) * 2; i < 3 * numOfFaces - 4 + (numOfFaces - 3); i = i + 2)
        {
            triangles[3 * i] = j + 1 + numOfFaces * 2;
            triangles[3 * i + 1] = j + numOfFaces * 2;
            triangles[3 * i + 2] = j;

            triangles[3 * i + 3] = j + 1 + numOfFaces * 2;
            triangles[3 * i + 4] = j;
            triangles[3 * i + 5] = j + 1;
            j += 2;
        }
        j = numOfFaces * 2;
        triangles[3 * (3 * numOfFaces - 3 + (numOfFaces - 3))] = j + numOfFaces * 2;
        triangles[3 * (3 * numOfFaces - 3 + (numOfFaces - 3)) + 1] = j + (numOfFaces * 2 - 1) + numOfFaces * 2;
        triangles[3 * (3 * numOfFaces - 3 + (numOfFaces - 3)) + 2] = j + (numOfFaces * 2 - 1);
        triangles[3 * (3 * numOfFaces - 3 + (numOfFaces - 3)) + 3] = j + numOfFaces * 2;
        triangles[3 * (3 * numOfFaces - 3 + (numOfFaces - 3)) + 4] = j + (numOfFaces * 2 - 1);
        triangles[3 * (3 * numOfFaces - 3 + (numOfFaces - 3)) + 5] = j;
    }
    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
