using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGeneration : MonoBehaviour
{
    [SerializeField]
    private const float RESPAWN_TIME = 1.0f;

    [SerializeField]
    private int PathLength = 10;

    [SerializeField]
    private List<GameObject> PathPrefabs;

    private Vector3 LastPathPosition;
    private Queue<GameObject> PathObjects = new Queue<GameObject>();
    private float TimeSinceLastObject;

    void Init()
    {
        LastPathPosition = new Vector3(0, 0, 0);
        TimeSinceLastObject = 0.0f;

        //populate list and spawn initial path
        for (int i = 0; i < PathLength; i++)
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
        TimeSinceLastObject += Time.deltaTime;

        //check if we should generate new path
        if (TimeSinceLastObject > RESPAWN_TIME)
        {
            //generate path
            SpawnPath();

            //destroy old one
            DestroyPath();

            //reset the time
            TimeSinceLastObject = 0.0f;
        }
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