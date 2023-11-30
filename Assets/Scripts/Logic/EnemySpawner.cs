using Data;
using Enemy;
using Infrastructure.Factory;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using Infrastructure.StaticData.Enemy;
using UI.Element;
using UnityEngine;

namespace Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public EnemyTypeID EnemyTypeID;
        private string _id;
        private bool _slain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        private StartBattle _startBattle;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }


        public void Construct(StartBattle startBattle)
        {
            _startBattle = startBattle;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }


        private void Spawn()
        {
            GameObject enemy = _factory.CreatEnemy(EnemyTypeID, transform);
            //enemy.GetComponentInChildren<EnemyMonkey>().Construct(_startBattle);
            _enemyDeath = enemy.GetComponentInChildren<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            _enemyDeath.Happened += Slay;
            _slain = false;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearSpawners.Add(_id);
        }
    }
}