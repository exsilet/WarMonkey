using Infrastructure.StaticData.Enemy;
using Infrastructure.StaticData.Players;

namespace Infrastructure.Service.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyStaticData ForTower(EnemyTypeID typeID);
        HeroStaticData ForPlayer(HeroTypeID typeID);
        LevelStaticData ForLevel(string sceneKey);
    }
}