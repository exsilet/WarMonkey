using Data;
using Enemy;
using Infrastructure.Factory;
using Infrastructure.Service.SaveLoad;
using Infrastructure.StaticData.Enemy;
using UI.Element;
using UnityEngine;

namespace Logic
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public EnemyTypeID EnemyTypeID;
        private IGameFactory _factory;
        private bool _slain;
        private EnemyDeath _enemyDeath;
        private StartBattle _startBattle;
        public string Id { get; set; }

        public void Construct(IGameFactory gameFactory)
        {
            _factory = gameFactory;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearSpawners.Contains(Id))
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
            if (_enemyDeath != null) 
                _enemyDeath.Happened -= Slay;

            _slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearSpawners.Add(Id);
        }
    }
}