using System;
using Data;
using Enemy;
using Infrastructure.Factory;
using Infrastructure.Service.SaveLoad;
using Infrastructure.StaticData.Enemy;
using UI.Element;
using UnityEngine;
using UnityEngine.Events;

namespace Logic
{
    public class SpawnPoint : MonoBehaviour
    {
        public EnemyTypeID EnemyTypeID;
        private IGameFactory _factory;
        private bool _slain;
        private int _enemyKilled;
        private EnemyDeath _enemyDeath;
        private StartBattle _startBattle;

        public bool Slain => _slain;
        public UnityAction<int> Slained;
        public string Id { get; set; }

        public void Construct(IGameFactory gameFactory)
        {
            _factory = gameFactory;
        }

        private void Start()
        {
            Spawn();
        }

        private void OnDestroy()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happened -= Slay;
        }

        private void Spawn()
        {
            GameObject enemy = _factory.CreatEnemy(EnemyTypeID, transform);
            _enemyDeath = enemy.GetComponentInChildren<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null) 
                _enemyDeath.Happened -= Slay;

            _slain = true;
            _enemyKilled++;
            Slained?.Invoke(_enemyKilled);
        }
    }
}