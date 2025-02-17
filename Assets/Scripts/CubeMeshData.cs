using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeMeshData
{
    public static Vector3[] vertices;
    
    public static int[][] faceTriangles =
    {
        new int[]{0,1,2,3},
        new int[]{5,0,3,6},
        new int[]{4,5,6,7},
        new int[]{1,4,7,2},
        new int[]{5,4,1,0},
        new int[]{3,2,7,6}

    };
    public static void SetVertices(float a, float b, float h)
    {
        a = a * 0.5f;
        b = b * 0.5f;
        h = h * 0.5f;
        //��������� ����� ���������� ������� ����������
        vertices = new Vector3[]
        {
            new Vector3(a,h,b),
            new Vector3(-a,h,b),
            new Vector3(-a,-h,b),
            new Vector3(a,-h,b),
            new Vector3(-a,h, -b),
            new Vector3(a,h, -b),
            new Vector3(a,-h, -b),
            new Vector3(-a,-h, -b)
        };
        
    }
    public static Vector3[] faceVertices(int dir)
    {
        Vector3[] fv = new Vector3[4];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = (vertices[faceTriangles[dir][i]]);
        }
        return fv;
    }
}
