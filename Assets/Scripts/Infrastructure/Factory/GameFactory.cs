using System.Collections.Generic;
using Enemy;
using Infrastructure.AssetManagement;
using Infrastructure.Service.SaveLoad;
using Infrastructure.Service.StaticData;
using Infrastructure.StaticData.Enemy;
using Logic;
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

        public GameObject CreateHero(GameObject at)
        {
            var hero = _assets.Instantiate(path: AssetPath.HeroPath, at: at.transform.position);

            RegisterProgressWatchers(hero);

            return hero;
        }

        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HudPath);
            RegisterProgressWatchers(hud);
            return hud;
        }

        // public GameObject CreateDraggableItem()
        // {
        //     var draggableItem = _assets.Instantiate(AssetPath.DraggableItemPath);
        //     return draggableItem;
        // }

        public GameObject CreateHudMenu()
        {
            GameObject hudMenu = _assets.Instantiate(AssetPath.HudMenuPath);
            RegisterProgressWatchers(hudMenu);
            return hudMenu;
        }

        public GameObject CreatEnemy(EnemyTypeID typeId, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForTower(typeId);
            GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);

            var health = enemy.GetComponent<IHealth>();
            health.Current = enemyData.Hp;
            health.Max = enemyData.Hp;
        
            return enemy;
        }

        public void Cleanup()
        {            
            ProgressReaders.Clear();
            ProgressWriters.Clear();
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