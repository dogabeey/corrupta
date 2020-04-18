using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDrawer : MonoBehaviour
{
    Material provinceMap, mat;
    float mapWidth, mapHeight;

    void Start()
    {
        provinceMap = GetComponent<Renderer>().material;
        //provinceMap.enabled = false;
        mapWidth = provinceMap.mainTexture.width;
        mapHeight = provinceMap.mainTexture.height;
    }

    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Texture2D tex = (Texture2D)provinceMap.mainTexture;
        if (Physics.Raycast(ray, out hit,256))
        {
            Debug.Log("Hit point: " + hit.textureCoord.x * mapWidth + " " + hit.textureCoord.y * mapHeight);
            SelectColor(tex.GetPixel((int)(hit.textureCoord.x * mapWidth), (int)(hit.textureCoord.y * mapHeight)));
        }

    }

    void SelectColor(Color color)
    {
        Destroy(GameObject.FindGameObjectWithTag("province"));

        Texture2D tmpTexture2;
        GameObject selectionMask = Instantiate(gameObject);
        selectionMask.layer = 9; // mask map layer
        selectionMask.GetComponent<MeshCollider>().enabled = false; // Prevent rays collide masks
        selectionMask.GetComponent<Animator>().enabled = true;
        selectionMask.tag = "province";
        Texture2D tmpTexture = (Texture2D)Instantiate(provinceMap.mainTexture, gameObject.transform.position, gameObject.transform.rotation);
        tmpTexture2 = (Texture2D)Instantiate(provinceMap.mainTexture);
        selectionMask.GetComponent<Renderer>().material.mainTexture = tmpTexture2;
        Color[] pixels = tmpTexture2.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            if(pixels[i] != color)
            {
                pixels[i].a = 0;
            }
        }
        tmpTexture2.SetPixels(pixels);
        tmpTexture2.Apply();

        provinceMap.mainTexture = tmpTexture;
    }
}