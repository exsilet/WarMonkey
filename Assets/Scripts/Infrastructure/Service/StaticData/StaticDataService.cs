using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticData.Enemy;
using Infrastructure.StaticData.Players;
using UnityEngine;

namespace Infrastructure.Service.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataEnemyPath = "StaticData/Enemy";
        private const string StaticDataHeroPath = "StaticData/Players";
        private const string LevelsDataPath = "StaticData/Levels";
        
        private Dictionary<EnemyTypeID, EnemyStaticData> _enemy;
        private Dictionary<HeroTypeID, HeroStaticData> _playerStatic;
        private Dictionary<string, LevelStaticData> _levels;

        public void Load()
        {
            _playerStatic = Resources
                .LoadAll<HeroStaticData>(StaticDataHeroPath)
                .ToDictionary(x => x.HeroTypeID, x => x);
            
            _enemy = Resources
                .LoadAll<EnemyStaticData>(StaticDataEnemyPath)
                .ToDictionary(x => x.EnemyTypeID, x => x);
            
            _levels = Resources
                .LoadAll<LevelStaticData>(LevelsDataPath)
                .ToDictionary(x => x.LevelKey, x => x);
        }
        
        public HeroStaticData ForPlayer(HeroTypeID typeID) =>
            _playerStatic.TryGetValue(typeID, out HeroStaticData staticData) 
                ? staticData 
                : null;

        public EnemyStaticData ForTower(EnemyTypeID typeID) =>
            _enemy.TryGetValue(typeID, out EnemyStaticData staticData) 
                ? staticData 
                : null;
        
        public LevelStaticData ForLevel(string sceneKey) => 
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData 
                : null;
    }
}