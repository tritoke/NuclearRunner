using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGeneration : MonoBehaviour
{
    [SerializeField]
    private float PATH_END_DISTANCE = 100.0f;
    private const float PATH_START_DISTANCE = -5.0f;

    [SerializeField]
    private List<GameObject> PathPrefabs;

    private PlayerController PlayerControllerRef;

    private Vector3 LastPathPosition;
    private Queue<GameObject> PathObjects = new Queue<GameObject>();

    void Init()
    {
        LastPathPosition = new Vector3(0, 0, 0);
        PlayerControllerRef = FindObjectOfType<PlayerController>();

        //populate list and spawn initial path
        while (PathNeedsExtending())
        {
            SpawnPath();
        }

    }

    void Start()
    {
        //get origin/start pos
        //populate list
        Init();
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
        var prefab = PathPrefabs[0];
        var renderer = prefab.GetComponent<MeshRenderer>();

        // calculate new position
        LastPathPosition += Vector3.Scale(renderer.bounds.size, Vector3.forward);

        // create instance
        var pathInstance = Instantiate(prefab, LastPathPosition, Quaternion.identity);

        // enqueue instance
        PathObjects.Enqueue(pathInstance);
    }

    private void DestroyPath()
    {
        Destroy(PathObjects.Dequeue());
    }
}