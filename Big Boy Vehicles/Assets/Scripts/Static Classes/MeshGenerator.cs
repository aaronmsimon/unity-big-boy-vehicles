using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateMesh(float[,] heightMap, float heightMultiplier)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        float[] zAxisNoise = new float[width];
        for (int i = 0; i < width; i++)
        {
            zAxisNoise[i] = heightMap[height - 1, i];
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if ((float)y / height <= zAxisNoise[x])
                {
                    meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightMap[x, y] * heightMultiplier, topLeftZ - y);
                    meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                    if (x < width - 1 && y < height - 1)
                    {
                        meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                        meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                    }
                }

                vertexIndex++;
            }
        }

        return meshData;
    }

}

/// <summary>
/// Creates the information needed to generate a custom mesh at run-time.
/// </summary>
public class MeshData
{
    // Properties
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    private int triangleIndex;

    // Constructor
    public MeshData(int width, int height)
    {
        vertices = new Vector3[width * height];
        triangles = new int[(width - 1) * (height - 1) * 6];
        uvs = new Vector2[width * height];
    }

    // Methods
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();

        return mesh;
    }
}