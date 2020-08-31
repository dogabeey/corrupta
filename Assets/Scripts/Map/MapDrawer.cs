using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class MapDrawer : MonoBehaviour
{
    public string textureName = "_MainTex";
    [NonSerialized] public Color selectedColor;

    Texture2D tex, originalTex;
    Material material;
    float mapHeight, mapWidth;

    void Start()
    {
        selectedColor = Color.black;
        material = GetComponent<Renderer>().material;
        originalTex = (Texture2D)material.GetTexture(textureName);
        mapHeight = originalTex.height;
        mapWidth = originalTex.width;
        tex = new Texture2D(originalTex.width, originalTex.height, originalTex.format, false);
        tex.SetPixels(originalTex.GetPixels());
    }

    void Update()
    {
    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,256))
        {
            Debug.Log("Hit point: " + hit.textureCoord.x * mapWidth + " " + hit.textureCoord.y * mapHeight);
            SelectColor(tex.GetPixel((int)(hit.textureCoord.x * mapWidth), (int)(hit.textureCoord.y * mapHeight)));
        }

    }

    void SelectColor(Color color)
    {
        selectedColor = color;
        Destroy(GameObject.FindGameObjectWithTag("province"));
        GameObject selectionMask = Instantiate(gameObject);
        selectionMask.layer = 9; // mask map layer
        selectionMask.GetComponent<MeshCollider>().enabled = false; // Prevent rays collide masks
        selectionMask.GetComponent<Animator>().enabled = true;
        selectionMask.tag = "province";
        Texture2D tex2 = new Texture2D(originalTex.width, originalTex.height, originalTex.format, false);
        tex2.SetPixels(originalTex.GetPixels());
        selectionMask.GetComponent<Renderer>().material.mainTexture = tex2;
        selectionMask.GetComponent<Renderer>().material.SetFloat("_BumpScale", 0);
        Color[] pixels = tex2.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            if(pixels[i] != color)
            {
                pixels[i].a = 0;
            }
        }
        tex2.SetPixels(pixels);
        tex2.Apply();
    }
}