using System.Collections.Generic;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using Infrastructure.StaticData.Enemy;
using Infrastructure.StaticData.Players;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateSelectUnits();
        GameObject CreateHero(HeroStaticData heroStaticData, Transform parent);
        GameObject CreateHud();
        GameObject CreateHudMenu();
        GameObject CreatEnemy(EnemyTypeID typeId, Transform parent);
        public GameObject CreateSpawner(string spawnerId, Vector3 at, EnemyTypeID enemyTypeID);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Register(ISavedProgressReader savedProgress);
        void Cleanup();
    }
}