using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class Tile : MonoBehaviour
{
    public int x, y;
    public string cityName;
    public List<Tile> neighbors = new List<Tile>();

    public City city;
    static GameObject activeTile;

    void Start()
    {
        cityName = city == null ? null : city.cityName;
    }

    void OnMouseDown()
    {
        if (activeTile != null)
        {
            activeTile.GetComponent<Outline>().eraseRenderer = true;
        }
        gameObject.GetComponent<Outline>().eraseRenderer = false;
        activeTile = gameObject;
        foreach (Tile tile in neighbors)
        {
            Debug.Log("Object " + tile.gameObject.GetHashCode());
        }
        WorldGenerator generator = FindObjectOfType<WorldGenerator>();

        StartCoroutine(generator.ConvertNeighbours(x, y, 500, 1.15));
    }

    void OnCollisionEnter(Collision collision)
    {
        neighbors.Add(collision.collider.gameObject.GetComponent<Tile>());
    }

    void OnCollisionExit(Collision collision)
    {
        neighbors.Remove(collision.collider.gameObject.GetComponent<Tile>());
    }
}
