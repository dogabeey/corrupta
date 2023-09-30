using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class MapDrawer : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public string provinceSelectString = "_ProvinceSelect";
    public string provinceColorString = "_ProvinceColor";
    [HideInInspector] public Color selectedColor;

    Texture2D tex, originalTex;
    float mapHeight, mapWidth;

    void Start()
    {
        mapHeight = originalTex.height;
        mapWidth = originalTex.width;
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
        meshRenderer.material.SetInt(provinceSelectString, color == Color.black ? 0 : 1);
        meshRenderer.material.SetColor(provinceColorString, color);

        selectedColor = color;
    }
}