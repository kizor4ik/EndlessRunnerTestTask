using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnManager : MonoBehaviour
{
    [Header("Injected components lower") ]
    public WorldParameters Parameters;
    public Transform PlayerTransform;
    public GameObject TilePrefab;
    public RoadLine[] Lines;
    public InteractableObject[] Things;

    // Tile spawn position with respect to Z axis.
    private float _spawnZ;
    private float _tileLenght;
    private List<GameObject> _activeTiles;

    [Inject]
    private void Construct(WorldParameters worldParameters, Player player, Tile tile, InteractableObject[] ObstaclesBoostsCookies, RoadLine[] linesInGame)
    {
        Parameters = worldParameters;
        PlayerTransform = player.transform;
        Things = ObstaclesBoostsCookies;
        Lines = linesInGame;
        TilePrefab = tile.gameObject;
    }

    void Start()
    {
        _activeTiles = new List<GameObject>();
        _tileLenght = TilePrefab.GetComponent<BoxCollider>().bounds.size.z;
        for (int i = 0; i < Parameters.AmmTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        BuildWorldFurther();
    }

    private void BuildWorldFurther()
    {
        if ((PlayerTransform.position.z - _tileLenght) > (_spawnZ - Parameters.AmmTilesOnScreen * _tileLenght))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile()
    {
        // Spawn tile.
        GameObject go;
        go = SimplePool.Spawn(TilePrefab);
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * _spawnZ;
        _spawnZ += _tileLenght;
        _activeTiles.Add(go);
        // Spawn things on the new Tile.
        SpawnThings(Parameters.AmountOfThings, go);
    }

    private void DeleteTile()
    {
        // Clear tile first.
        foreach (Transform child in _activeTiles[0].transform)
        {
            SimplePool.Despawn(child.gameObject);
        }
        // And remove the tile.
        SimplePool.Despawn(_activeTiles[0]);
        _activeTiles.RemoveAt(0);
    }

    public void SpawnThings(int amount, GameObject onTile)
    {
        // Spawn objects on some tile.
        for (int i = 0; i < amount; i++)
        {
            // Pick random thing.
            int thingIndex = Random.Range(0, Things.Length);
            GameObject thing = SimplePool.Spawn(Things[thingIndex].gameObject);
            thing.transform.SetParent(onTile.transform);

            // Pick random line.
            int lineIndex = Random.Range(0, Lines.Length);

            // Evaluate thing's position.
            Vector3 thingPosition = new Vector3(Lines[lineIndex].transform.position.x, thing.transform.position.y, onTile.transform.position.z + i * _tileLenght / amount);
            if (thingPosition.z > Parameters.SafeZone)
            {
                thing.transform.position = thingPosition;
            }
            else
            {
               SimplePool.Despawn(thing);
            }
        }
       
    }
}
