using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DrawDirt))]
public class DirtGenerator : MonoBehaviour
{
    private enum DrawMode { NoiseMap, ColorMap, Mesh };
    [SerializeField] DrawMode drawMode;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float noiseScale;
    [SerializeField] private int octaves;
    [SerializeField] [Range(0, 1)] private float persistance;
    [SerializeField] float lacunarity;
    [SerializeField] int seed;
    [SerializeField] Vector2 offset;
    [SerializeField] float meshHeightMultiplier;
    [SerializeField] float meshFalloffBuffer;

    public bool autoUpdate;

    [SerializeField] TerrainType[] regions;

    public void GenerateDirt()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, seed, noiseScale, octaves, persistance, lacunarity, offset);

        float[] zAxisNoise = new float[width];
        for (int i = 0; i < width; i++)
        {
            zAxisNoise[i] = noiseMap[height - 1, i];
        }
        float[,] newNoiseMap = new float[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                newNoiseMap[x, y] = (float)y / height < zAxisNoise[x] ? (float)(height - y) / height : ((float)y / height <= zAxisNoise[x] + meshFalloffBuffer ? (zAxisNoise[x] + meshFalloffBuffer) - (float)y / height : 0);
            }
        }

        

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float currentHeight = newNoiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        if (currentHeight > 0)
                        {
                            colorMap[y * width + x] = regions[i].colour;
                        } else
                        {
                            colorMap[y * width + x] = Color.red;
                        }
                        break;
                    }
                }
            }
        }

        DrawDirt display = FindObjectOfType<DrawDirt>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(newNoiseMap));
        } else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, width, height));
        } else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateMesh(newNoiseMap, meshHeightMultiplier), TextureGenerator.TextureFromColorMap(colorMap, width, height));
        }
    }

    void OnValidate()
    {
        if (width < 1)
        {
            width = 1;
        }
        if (height < 1)
        {
            height = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}