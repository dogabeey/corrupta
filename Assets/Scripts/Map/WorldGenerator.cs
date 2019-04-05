using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI.Extensions;

public class WorldGenerator : MonoBehaviour
{
    public int x, y;
    public Vector2[,] points;
    public GameObject dotPrefab;
    public Material green, coast, sea;

    int[,] adjMatrix;
    public float mapHeight, mapWidth;

    public void Start()
    {
        points = new Vector2[x, y];
        adjMatrix = new int[points.GetLength(0) * points.GetLength(1), points.GetLength(0) * points.GetLength(1)];
        GeneratePoints();
        DrawPoints();
    }

    public void GeneratePoints()
    {
        int xLength = points.GetLength(0);
        int yLength = points.GetLength(1);
        float multiplierX = mapWidth / xLength;
        float multiplierY = mapHeight / yLength;
        for (int i = 0; i < xLength; i++)
        {
            for (int j = 0; j < yLength; j++)
            {
                points[i, j] = new Vector2(i * multiplierX + UnityEngine.Random.Range(0, multiplierX), j * multiplierY + UnityEngine.Random.Range(0, multiplierY));

            }
        }
    }

    public void DrawPoints()
    {
        int xLength = points.GetLength(0);
        int yLength = points.GetLength(1);
        for (int i = 0; i < xLength -1; i++)
        {
            for (int j = 0; j < yLength -1; j++)
            {
                GameObject prefab = Instantiate(dotPrefab);

                Mesh mesh = new Mesh();
                MeshFilter meshFilter = prefab.GetComponent<MeshFilter>();
                Tile tile = prefab.GetComponent<Tile>();
                Vector3[] vertices = new Vector3[4];
                Vector3[] normals = new Vector3[4];
                Vector2[] uv = new Vector2[4];
                int[] tri = new int[6];

                vertices[0] = points[i, j + 1];
                vertices[1] = points[i + 1, j + 1];
                vertices[2] = points[i, j];
                vertices[3] = points[i + 1, j];

                mesh.vertices = vertices;

                //  Lower left triangle.
                tri[0] = 0;
                tri[1] = 2;
                tri[2] = 1;

                //  Upper right triangle.   
                tri[3] = 2;
                tri[4] = 3;
                tri[5] = 1;

                mesh.triangles = tri;

                normals[0] = -Vector3.forward;
                normals[1] = -Vector3.forward;
                normals[2] = -Vector3.forward;
                normals[3] = -Vector3.forward;

                mesh.normals = normals;

                uv[0] = new Vector2(0, 0);
                uv[1] = new Vector2(1, 0);
                uv[2] = new Vector2(0, 1);
                uv[3] = new Vector2(1, 1);

                mesh.uv = uv;
                meshFilter.mesh = mesh;

                meshFilter.gameObject.AddComponent<MeshCollider>();

                tile.x = i;
                tile.y = j;
            }
        }
    }

    public void DrawPolygons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("ball");
        Vector2 point1, point2, point3, point4;

        int xLength = points.GetLength(0);
        int yLength = points.GetLength(1);
        for (int i = 0; i < xLength - 1; i++)
        {
            for (int j = 0; j < yLength - 1; j++)
            {
                Debug.Log("Drawing a line starting at " + points[i, j]);
                point1 = points[i, j];
                point2 = points[i + 1, j];
                point3 = points[i + 1, j + 1];
                point4 = points[i, j + 1];
                Debug.DrawLine(point1, point2,Color.red,Mathf.Infinity);
                Debug.DrawLine(point2, point3, Color.red, Mathf.Infinity);
                Debug.DrawLine(point3, point4, Color.red, Mathf.Infinity);
                Debug.DrawLine(point4, point1, Color.red, Mathf.Infinity);
            }
        }
    }

    public IEnumerator ConvertNeighbours(int x, int y, double probability, double divide)
    {
        GameObject currentTile = FindObjectsOfType<Tile>().ToList().Find(t => t.x == x && t.y == y).gameObject;
        currentTile.GetComponent<Renderer>().material.color = Color.green;
        if (x <= 0 || x >= points.GetLength(0) - 1 || y <= 0 || y >= points.GetLength(1) - 1) yield break;
        Tile neighbor_north = FindObjectsOfType<Tile>().ToList().Find(t => t.x == x && t.y == y - 1);
        Tile neighbor_east = FindObjectsOfType<Tile>().ToList().Find(t => t.x == x + 1 && t.y == y);
        Tile neighbor_south = FindObjectsOfType<Tile>().ToList().Find(t => t.x == x && t.y == y + 1);
        Tile neighbor_west = FindObjectsOfType<Tile>().ToList().Find(t => t.x == x - 1 && t.y == y);
        // FTS. I'm hardcoding this.
        // EAST
        if (probability > UnityEngine.Random.Range(0, 50) + new System.Random().Next(0, 50) && neighbor_east.GetComponent<Renderer>().material.color != Color.green)
        {
            neighbor_east.GetComponent<Renderer>().material.color = Color.green;
            StartCoroutine(ConvertNeighbours(x + 1, y, probability / divide, divide));
            yield return new WaitForSeconds(0.1f);
        }
        // NORTH
        if (probability > UnityEngine.Random.Range(0, 50) + new System.Random().Next(0,50) && neighbor_north.GetComponent<Renderer>().material.color != Color.green)
        {
            neighbor_north.GetComponent<Renderer>().material.color = Color.green;
            StartCoroutine(ConvertNeighbours(x, y - 1, probability / divide, divide));
            yield return new WaitForSeconds(0.1f);

        }
        // WEST
        if (probability > UnityEngine.Random.Range(0, 50) + new System.Random().Next(0, 50) && neighbor_west.GetComponent<Renderer>().material.color != Color.green)
        {
            neighbor_west.GetComponent<Renderer>().material.color = Color.green;
            StartCoroutine(ConvertNeighbours(x - 1, y, probability / divide, divide));
            yield return new WaitForSeconds(0.1f);
        }
        // SOUTH
        if (probability > UnityEngine.Random.Range(0, 50) + new System.Random().Next(0, 50) && neighbor_south.GetComponent<Renderer>().material.color != Color.green)
        {
            neighbor_south.GetComponent<Renderer>().material.color = Color.green;
            StartCoroutine(ConvertNeighbours(x, y + 1, probability / divide, divide));
            yield return new WaitForSeconds(0.1f);
        }

    }

    public void DemoConvert()
    {
        StartCoroutine(ConvertNeighbours(points.GetLength(0)/2, points.GetLength(1)/2, 400, 1.3));
        StartCoroutine(ConvertNeighbours(points.GetLength(0)/2, points.GetLength(1)/3, 400, 1.3));
        StartCoroutine(ConvertNeighbours(points.GetLength(0)/3, points.GetLength(1)/2, 400, 1.3));
        StartCoroutine(ConvertNeighbours(points.GetLength(0)/3, points.GetLength(1)/3, 400, 1.3));
    }

    void OnMouseDown()
    {
        
    }
}
