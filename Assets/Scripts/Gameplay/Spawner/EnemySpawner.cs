using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    [SerializeField] EnemySpawnerData data;

    private float currentTime;
    private float currentDifficultyTime;
    private float currentWaveTime;
    private float currentOnWaveTime;

    float spawnEnemyDelay;

    private List<Enemy> enemyInstantiate = new();

    private int lifeIncrease;

    private void Start()
    {
        data = Instantiate(data);
        spawnEnemyDelay = data.spawnEnemyDelay;

    }
    private void Update()
    {
        if (currentTime >= data.spawnEnemyDelay && enemyInstantiate.Count < data.maxEnemy)
        {
            currentTime = 0;
            SpawnEnemy();
        }

        if(currentDifficultyTime >= data.difficultyIncreaseDelay)
        {
            currentDifficultyTime = 0;
            lifeIncrease += data.lifeIncrease;
        }

        if (currentWaveTime >= data.waveDelay)
        {
            if (currentOnWaveTime == 0)
            {
                data.spawnEnemyDelay = data.spawnEnemyDelayOnWave;
            }

            currentOnWaveTime += Time.deltaTime;
            
            if (currentOnWaveTime >= data.waveDuration)
            {
                currentWaveTime = 0;
                currentOnWaveTime = 0;

                data.spawnEnemyDelay = spawnEnemyDelay;
            }
        }

        currentTime += Time.deltaTime;
        currentDifficultyTime += Time.deltaTime;
        currentWaveTime += Time.deltaTime;
    }

    private void SpawnEnemy()
    {
        bool validSpawn = false;
        Vector3 spawnPosition = Vector3.zero;

        for (int i = 0; i < 50; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(data.distanceMinMaxSpawn.x, data.distanceMinMaxSpawn.y);

            spawnPosition = transform.position + new Vector3(Mathf.Cos(angle) * distance, 0, Mathf.Sin(angle) * distance);

            if (!Physics.Raycast(transform.position + Vector3.up * .5f, (spawnPosition - transform.position).normalized, Vector3.Distance(transform.position, spawnPosition), LayerMask.NameToLayer("Wall")))
            {
                validSpawn = true;
                break;
            }
        }

        if (validSpawn)
        {
            Enemy newEnemy = Instantiate(data.enemies[Random.Range(0,data.enemies.Count)], spawnPosition, Quaternion.identity);
            newEnemy.enemyData = Instantiate(newEnemy.enemyData);
            newEnemy.enemyData.enemyLife += lifeIncrease;
        }
    }

    private void RemoveEnemy(Enemy enemy)
    {
        enemyInstantiate.Remove(enemy);
    }
}