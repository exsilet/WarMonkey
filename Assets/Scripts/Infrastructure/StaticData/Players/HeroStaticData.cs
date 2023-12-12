using UnityEngine;

namespace Infrastructure.StaticData.Players
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero")]
    public class HeroStaticData : ScriptableObject
    {
        public float Hp;
        public float Speed;

        public Sprite UIIcon;
        public GameObject Prefab;
        public HeroTypeID HeroTypeID;
    }

    public enum HeroTypeID
    {
        Regular = 0,
        Doctor,
        PoliceOfficer,
        Cook,
        Firefighter,
        OfficeWorker
    }
}