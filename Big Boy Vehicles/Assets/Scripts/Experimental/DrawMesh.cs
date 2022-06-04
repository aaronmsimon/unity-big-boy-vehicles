using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DrawMesh : MonoBehaviour
{
    private MeshFilter meshFilter;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        //float[,] noiseMap = Noise.GenerateNoiseMap(10, 10, 1, 1, 1, 1, 1, new Vector2(0, 0));

        //MeshData meshData = MeshGenerator.GenerateMesh(noiseMap);
        //meshFilter.sharedMesh = meshData.CreateMesh();
    }
}
