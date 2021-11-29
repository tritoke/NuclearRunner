using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGeneration : MonoBehaviour
{
    [SerializeField]
    private float PATH_END_DISTANCE = 1000.0f;
    private float PATH_START_DISTANCE;

    [SerializeField]
    private List<GameObject> PathPrefabs = new List<GameObject>();

    [SerializeField]
    private List<GameObject> ObstaclePrefabs = new List<GameObject>();

    private Vector3 LastPathPosition;
    private Queue<GameObject> PathObjects = new Queue<GameObject>();
    private int NumTilesInstantiated = 0;

    private PlayerController PlayerControllerRef;

    void Start()
    {
        PlayerControllerRef = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        Debug.Log(PathPrefabs[0]);

        var prefab = PathPrefabs[0];
        var renderer = prefab.GetComponent<MeshRenderer>();
        PATH_START_DISTANCE = -1 * renderer.bounds.size.z;

        LastPathPosition = Vector3.forward * PATH_START_DISTANCE;

        //populate list and spawn initial path
        while (PathNeedsExtending())
        {
            SpawnPath();
        }

        // var pcr = transform.parent;
        // Debug.Log($"pcr = {pcr}");
    }

    void Update()
    {
        //check if we should generate new path
        while (PathNeedsExtending())
        {
            //generate path
            SpawnPath();
        }

        //check if we can delete path
        while (PathOutOfView())
        {
            //remove path
            DestroyPath();
        }
    }

    private bool PathNeedsExtending()
    {
        //get distance between player and last added path object
        var playerPos = PlayerControllerRef.transform.position;
        var difference = Vector3.Scale(playerPos - LastPathPosition, Vector3.forward).magnitude;
        return difference < PATH_END_DISTANCE;
    }

    private bool PathOutOfView()
    {
        //get distance between player and last path object
        var playerZPos = PlayerControllerRef.transform.position.z;
        var lastTileZPos = PathObjects.Peek().transform.position.z;
        var zDiff = lastTileZPos - playerZPos;
        return zDiff < PATH_START_DISTANCE;
    }

    private void SpawnPath()
    {
        // choose prefab
        var prefab = PathPrefabs[UnityEngine.Random.Range(0, PathPrefabs.Count)];
        var renderer = prefab.GetComponent<MeshRenderer>();

        // calculate new position
        LastPathPosition += Vector3.Scale(renderer.bounds.size, Vector3.forward);

        // create instance
        var tile = Instantiate(prefab, LastPathPosition, Quaternion.identity);
        NumTilesInstantiated++;

        // generate obstacles
        var tileController = tile.GetComponent<TileController>();
        if (NumTilesInstantiated > 5)
        {
            var pattern = GetRandomObstacle();

            tileController.GenerateObstaclePattern(pattern, ObstaclePrefabs[0]);
        }

        // enqueue instance
        PathObjects.Enqueue(tile);
    }

    private TileController.ObstaclePattern GetRandomObstacle()
    {
        var variants = Enum.GetValues(typeof(TileController.ObstaclePattern));
        var index = UnityEngine.Random.Range(0, variants.Length);
        return (TileController.ObstaclePattern) variants.GetValue(index);
    }

    private void DestroyPath()
    {
        Destroy(PathObjects.Dequeue());
    }
}