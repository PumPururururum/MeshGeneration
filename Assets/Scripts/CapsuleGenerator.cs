using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CapsuleGenerator : MonoBehaviour
{
    private int segments;
    Color color;

    private float height;
    private float radius;

    Mesh mesh;

    int[] triangles;
    Vector3[] vertices;

    int points;
    float[] pX;
    float[] pZ;
    float[] pY;
    float[] pR;

    public void Generator(float radius, float height, int segments, Color color)
    {
        this.radius = radius;
        this.height = height;
        this.segments = segments;
        this.color = color;
        Initialize();
        GenerateMesh();
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
        if (mesh == null || mf == null)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

        }      
       
    }
    private void CalculatePoints()
    {
        if (segments % 2 != 0)
            segments++;

        // extra vertex on the seam
        points = segments + 1;

        // calculating points around a circle
        pX = new float[points];
        pZ = new float[points];
        pY = new float[points];
        pR = new float[points];

        float calcH = 0f;
        float calcV = 0f;

        for (int i = 0; i < points; i++)
        {
            pX[i] = Mathf.Sin(calcH * Mathf.Deg2Rad);
            pZ[i] = Mathf.Cos(calcH * Mathf.Deg2Rad);
            pY[i] = Mathf.Cos(calcV * Mathf.Deg2Rad);
            pR[i] = Mathf.Sin(calcV * Mathf.Deg2Rad);

            calcH += 360f / (float)segments;
            calcV += 180f / (float)segments;
        }
    }
    private void GenerateVertices()
    {
        //Vertices
        vertices = new Vector3[points * (points + 1)];
        int ind = 0;

        // Y-offset is half the height minus diameter
        float yOff = (height - (radius * 2f)) * 0.5f;
        if (yOff < 0)
            yOff = 0;

        // Top Hemisphere
        int top = Mathf.CeilToInt(points * 0.5f);

        for (int y = 0; y < top; y++)
        {
            for (int x = 0; x < points; x++)
            {
                vertices[ind] = new Vector3(pX[x] * pR[y], pY[y], pZ[x] * pR[y]) * radius;
                vertices[ind].y = yOff + vertices[ind].y;

                ind++;
            }
        }

        // Bottom Hemisphere
        int btm = Mathf.FloorToInt(points * 0.5f);

        for (int y = btm; y < points; y++)
        {
            for (int x = 0; x < points; x++)
            {
                vertices[ind] = new Vector3(pX[x] * pR[y], pY[y], pZ[x] * pR[y]) * radius;
                vertices[ind].y = -yOff + vertices[ind].y;

                ind++;
            }
        }
    }
    private void GenerateTriangles()
    {
        triangles = new int[(segments * (segments + 1) * 2 * 3)];

        for (int y = 0, t = 0; y < segments + 1; y++)
        {
            for (int x = 0; x < segments; x++, t += 6)
            {
                triangles[t + 0] = ((y + 0) * (segments + 1)) + x + 0;
                triangles[t + 1] = ((y + 1) * (segments + 1)) + x + 0;
                triangles[t + 2] = ((y + 1) * (segments + 1)) + x + 1;

                triangles[t + 3] = ((y + 0) * (segments + 1)) + x + 1;
                triangles[t + 4] = ((y + 0) * (segments + 1)) + x + 0;
                triangles[t + 5] = ((y + 1) * (segments + 1)) + x + 1;
            }
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    private void GenerateColor()
    {
        GetComponent<MeshRenderer>().sharedMaterial.color = color;
    }
    private void GenerateMesh()
    {
        CalculatePoints();
        GenerateVertices();
        GenerateTriangles();
    }
}
