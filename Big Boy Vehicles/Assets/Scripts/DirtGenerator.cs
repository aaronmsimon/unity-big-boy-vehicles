using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DrawDirt))]
public class DirtGenerator : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int heightMultiplier;
    [SerializeField] private int noiseScale;
    [SerializeField] private int amplitude;
    [SerializeField] private int offset;
    [SerializeField] private int seed;
    [SerializeField] private float meshHeightMultiplier;

    public bool autoUpdate;

    public void GenerateDirt()
    {
        System.Random prng = new System.Random(seed);

        int height = (amplitude + noiseScale) * heightMultiplier;
        float[,] noiseMap = new float[width, height];

        float[] yAxis = new float[width];
        for (int x = 0; x < width; x++)
        {
            yAxis[x] = (Mathf.Sin(x + offset) * amplitude + Random.Range(0f, noiseScale)) * heightMultiplier;
                Debug.Log(yAxis[x]);
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[x, y] = yAxis[x] > y ? 1 : 0;
            }
        }
        DrawDirt display = GetComponent<DrawDirt>();
        display.DrawMesh(MeshGenerator.GenerateMesh(noiseMap, meshHeightMultiplier));
    }

    void OnValidate()
    {
        if (width < 1)
        {
            width = 1;
        }
        if (noiseScale < 0)
        {
            noiseScale = 0;
        }
        if (amplitude < 1)
        {
            amplitude = 1;
        }
    }
}
