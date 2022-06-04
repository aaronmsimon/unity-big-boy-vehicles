using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawDirt : MonoBehaviour
{
    [SerializeField] private Renderer textureRenderer;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Texture2D texture;

    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        //meshRenderer.sharedMaterial.mainTexture = texture;
        meshRenderer.sharedMaterial.mainTexture = texture;
    }

}
