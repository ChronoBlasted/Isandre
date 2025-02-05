using System;
using UnityEngine;
using System.Collections;
using Unity.Collections;
using System.Collections.Generic;

public class EnemyDistance : Enemy
{
    public List<Spawner> spawnerList;

    public override void Awake()
    {
        base.Awake();
        inAction = false;
    }

    public override void Attack()
    {
        base.Attack();

        foreach (var v in spawnerList)
            v.SpawnObject(enemyData.Projectile);

    }
}



