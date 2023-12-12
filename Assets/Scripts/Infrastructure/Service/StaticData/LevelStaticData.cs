using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Service.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<EnemySpawnerStaticData> EnemySpawners;
    }
}