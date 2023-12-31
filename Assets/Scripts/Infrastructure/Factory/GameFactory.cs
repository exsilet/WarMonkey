﻿using System.Collections.Generic;
using Enemy;
using Infrastructure.AssetManagement;
using Infrastructure.Service.SaveLoad;
using Infrastructure.Service.StaticData;
using Infrastructure.StaticData.Enemy;
using Infrastructure.StaticData.Players;
using Logic;
using Player;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject _heroGameObject;
        
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateSelectUnits()
        {
            var selectUnits = InstantiateRegistered(AssetPath.SelectUnitsPath);
            return selectUnits;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);
            
            return hud;
        }

        public GameObject CreateHero(HeroStaticData heroStaticData, Transform parent)
        {
            HeroStaticData heroData = _staticData.ForPlayer(heroStaticData.HeroTypeID);
            GameObject hero = Object.Instantiate(heroData.Prefab, parent.position, Quaternion.identity, parent);

            var health = hero.GetComponentInChildren<IHealth>();
            health.Current = heroData.Hp;
            health.Max = heroData.Hp;

            RegisterProgressWatchers(hero);

            return hero;
        }

        public GameObject CreateSpawner(string spawnerId, Vector3 at, EnemyTypeID enemyTypeID)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at).GetComponent<SpawnPoint>();
      
            spawner.Construct(this);
            spawner.EnemyTypeID = enemyTypeID;
            spawner.Id = spawnerId;

            return spawner.gameObject;
        }

        public GameObject CreateHudMenu()
        {
            GameObject hudMenu = InstantiateRegistered(AssetPath.HudMenuPath);
            return hudMenu;
        }

        public GameObject CreatEnemy(EnemyTypeID typeId, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForTower(typeId);
            GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);

            var health = enemy.GetComponentInChildren<IHealth>();
            health.Current = enemyData.Hp;
            health.Max = enemyData.Hp;
            var enemySpeed = enemy.GetComponentInChildren<EnemyMovement>();
            enemySpeed.Speed = enemyData.Speed;
            var enemyMoney = enemy.GetComponentInChildren<LootEnemy>();
            enemyMoney.Money = enemyData.Reward;
        
            return enemy;
        }

        public void Cleanup()
        {            
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
        
        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }
    }
}