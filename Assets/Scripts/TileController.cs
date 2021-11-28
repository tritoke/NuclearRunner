using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public static int ObstaclesPassed { get; private set; } = 0;

    [SerializeField]
    private GameObject SpawnPointL;

    [SerializeField]
    private GameObject SpawnPointM;

    [SerializeField]
    private GameObject SpawnPointR;

    public enum ObstaclePattern
    {
        Left,
        Middle,
        Right,
        LowBar,
    };

    private bool HasObstacle = false;

    public void GenerateObstaclePattern(ObstaclePattern pattern, GameObject obstaclePrefab)
    {
        HasObstacle = true;

        switch (pattern) 
        {
            case ObstaclePattern.Left:
                GenerateObstacleAtPoint(SpawnPointL, obstaclePrefab);
                break;

            case ObstaclePattern.Middle:
                GenerateObstacleAtPoint(SpawnPointM, obstaclePrefab);
                break;

            case ObstaclePattern.Right:
                GenerateObstacleAtPoint(SpawnPointR, obstaclePrefab);
                break;

            case ObstaclePattern.LowBar:
                GenerateObstacleAtPoint(SpawnPointL, obstaclePrefab);
                GenerateObstacleAtPoint(SpawnPointM, obstaclePrefab);
                GenerateObstacleAtPoint(SpawnPointR, obstaclePrefab);
                break;
        }
    }

    private void GenerateObstacleAtPoint(GameObject point, GameObject prefab)
    {
        var obstacle = Instantiate(prefab, point.transform.position, Quaternion.identity);
        obstacle.transform.SetParent(transform);
    }

    void OnDelete()
    {
        ObstaclesPassed++;
    }
}
