using UnityEngine;

namespace Infrastructure.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public float Hp;
        public int Damage;
        public float Cooldown;
        public float Speed;
        public int Reward;
        
        public Sprite UIIcon;
        public GameObject Prefab;
        public EnemyTypeID EnemyTypeID;
    }
}

