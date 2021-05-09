using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnManager : MonoBehaviour
{
    // Amount of objects per tile
    public int amountOfThings = 10;
    // Amount of tiles simulthaniously
    public int ammTilesOnScreen = 2;
    // Initial zone without object's
    public float safeZone = 10f;

    [Header("Injected components lower") ]
    public Transform playerTransform;
    public GameObject tilePrefab;
    public Transform[] lines;
    public GameObject[] things;

    // Position for tile spawn with respect to Z axis
    private float spawnZ;
    private float tileLenght;
    private List<GameObject> activeTiles;

    [Inject]
    private void Contruct(Player player, GameObject tile, GameObject[] ObstaclesBoostsCookies, Transform[] linesInGame)
    {
        playerTransform = player.transform;
        things = ObstaclesBoostsCookies;
        lines = linesInGame;
        tilePrefab = tile;
    }

    void Start()
    {
        activeTiles = new List<GameObject>();
        tileLenght = tilePrefab.GetComponent<BoxCollider>().bounds.size.z;
        for (int i = 0; i < ammTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (playerTransform.position.z-tileLenght > (spawnZ - ammTilesOnScreen * tileLenght))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile()
    {
        GameObject go;
        go = Instantiate(tilePrefab) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLenght;
        activeTiles.Add(go);
        // Spawn things on new Tile
        SpawnThings(amountOfThings, go);
    }

    private void DeleteTile()
    {
        // Clear tile first
        Transform[] childsToDelete = activeTiles[0].GetComponentsInChildren<Transform>();
        foreach (Transform child in childsToDelete)
        {
            Destroy(child.gameObject);          
        }
        // Then destroy Tile
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    public void SpawnThings(int amount, GameObject onTile)
    {
        for (int i = 0; i < amount; i++)
        {
            int thingIndex = Random.Range(0, things.Length);
            GameObject thing = Instantiate(things[thingIndex]) as GameObject;
            thing.transform.SetParent(onTile.transform);
            int lineIndex = Random.Range(0, lines.Length);
            Vector3 thingPosition = new Vector3(lines[lineIndex].position.x, thing.transform.position.y, onTile.transform.position.z + i * tileLenght / amount);
            if (thingPosition.z > safeZone)
            {
                thing.transform.position = thingPosition;
            }
            else
            {
                Destroy(thing);
            }
        }
    }
}
