using System;
using Infrastructure.StaticData.Enemy;
using UnityEngine;

namespace Infrastructure.Service.StaticData
{
    [Serializable]
    public class EnemySpawnerStaticData
    {
        public string Id;
        public EnemyTypeID EnemyTypeID;
        public Vector3 Position;

        public EnemySpawnerStaticData(string id, EnemyTypeID enemyTypeID, Vector3 position)
        {
            Id = id;
            EnemyTypeID = enemyTypeID;
            Position = position;
        }
    }
}