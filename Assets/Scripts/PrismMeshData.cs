using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrismMeshData
{
    private static Vector3[] vertices;
   // private static int[][] faceTriangles;

    public static void SetVertices(int numOfFaces, float height, float radious)
    {

        vertices = new Vector3[numOfFaces * 2];
        float edgeLength = 2 * radious * Mathf.Sin(Mathf.PI / numOfFaces);
        float angle = 2 * Mathf.PI / numOfFaces;
        for (int i = 0; i < numOfFaces; i++)
        {
            vertices[i] = new Vector3(Mathf.Sin(i*angle) * radious, 0,Mathf.Cos(i*angle) * radious) ;
        }
        for (int i = numOfFaces;i < numOfFaces*2; i++)
        {
            vertices[i] = new Vector3(Mathf.Sin(i * angle) * radious, height, Mathf.Cos(i * angle) * radious);
        }
    }
    public static void SetTriangles(int numOfFaces)
    {
        int[] triangles = new int[3 * (numOfFaces - 2)];
        for (int i = 0;i < numOfFaces -2; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }
    }

}
