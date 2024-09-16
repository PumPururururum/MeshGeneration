using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class CubeGenerator : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    private float lengthX, lenghtY, lengthZ;
    Color color;

    public void Generator(float lengthX, float lenghtY, float lengthZ, Color color)
    {
        this.lengthX = lengthX;
        this.lenghtY = lenghtY;
        this.lengthZ = lengthZ;
        this.color = color;
        Initialize();
        MakeCube();
        UpdateMesh();
        GenerateColor();
    }
    public void ClearMesh()
    {
        mesh.Clear();
    }
    private void Initialize()
    {
        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        mesh = mf.sharedMesh;
        if (mesh == null)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }
    }

    private void MakeCube()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        CubeMeshData.SetVertices(lengthX, lengthZ, lenghtY);

        for (int i = 0; i  < 6; i++)
        {
            MakeFace(i);
        }
    }
    private void MakeFace(int dir)
    {
        vertices.AddRange(CubeMeshData.faceVertices(dir));

        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 4 + 1);
        triangles.Add(vertices.Count - 4 + 2);
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 4 + 2);
        triangles.Add(vertices.Count - 4 + 3);

    }
    private void GenerateColor()
    {
        GetComponent<MeshRenderer>().sharedMaterial.color = color;
    }
    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}
