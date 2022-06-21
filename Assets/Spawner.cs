using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private int maxFoodInScreen;

    [SerializeField] private GameObject bigEnemyPrefabs;
    [SerializeField] private GameObject smallEnemyPrefabs;
    [SerializeField] private int maxEnemyInScreen;

    void Start()
    {
        SpawnFood();
        SpawnEnemy();
        Destroy(gameObject);
    }

    private void SpawnFood()
    {
        for (int i = 0; i < maxFoodInScreen; i++)
        {
            float spawnY = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < maxEnemyInScreen; i++)
        {
            float spawnY = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            var enemyToSpawn = Random.Range(1, 3);
            if(enemyToSpawn == 1) Instantiate(bigEnemyPrefabs, spawnPosition, Quaternion.identity);
            else Instantiate(smallEnemyPrefabs, spawnPosition, Quaternion.identity);

        }
    }
}
