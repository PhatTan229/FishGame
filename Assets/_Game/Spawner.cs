using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> walls;

    float nextSpawnWall;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Time.time > nextSpawnWall)
        {
            nextSpawnWall += Random.Range(2f, 3f);
            string objectName = Random.Range(0, 2) == 0 ? "up" : "down";
            ObjectPool.Instance.GetGameObjectFromPool(objectName, new Vector3(6, 0));
        }
    }
}
