using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> walls;

    float nextSpawnWall;
    float nextSpawnPuppetPoint;

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

        if (Time.time > nextSpawnPuppetPoint)
        {
            nextSpawnPuppetPoint += Random.Range(10f, 12f);
            ObjectPool.Instance.GetGameObjectFromPool("savePlayerPuppet", new Vector3(6, Random.Range(-1f,1f)));
        }
    }
}
