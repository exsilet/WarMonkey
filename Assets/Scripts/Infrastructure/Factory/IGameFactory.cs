using System.Collections.Generic;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using Infrastructure.StaticData.Enemy;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        GameObject CreateHudMenu();
        GameObject CreatEnemy(EnemyTypeID typeId, Transform parent);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Register(ISavedProgressReader savedProgress);
        void Cleanup();
    }
}