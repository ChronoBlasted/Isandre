using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerData", menuName = "Data/EnemySpawnerData", order = 1)]
public class EnemySpawnerData : ScriptableObject
{
    public int maxEnemy;
    public List<Enemy> enemies;
    public Vector2 distanceMinMaxSpawn;

    public float spawnEnemyDelay;

    [Header("Difficulty")]
    public float difficultyIncreaseDelay;
    public int lifeIncrease;

}
