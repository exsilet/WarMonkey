using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticData.Enemy;
using UnityEngine;

namespace Infrastructure.Service.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataTowerPath = "StaticData/Enemy";
        private Dictionary<EnemyTypeID, EnemyStaticData> _enemy;

        public void Load()
        {
            _enemy = Resources
                .LoadAll<EnemyStaticData>(StaticDataTowerPath)
                .ToDictionary(x => x.EnemyTypeID, x => x);
        }

        public EnemyStaticData ForTower(EnemyTypeID typeID) =>
            _enemy.TryGetValue(typeID, out EnemyStaticData staticData) 
                ? staticData 
                : null;
    }
}