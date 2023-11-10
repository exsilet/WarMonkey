using Infrastructure.StaticData.Enemy;

namespace Infrastructure.Service.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyStaticData ForTower(EnemyTypeID typeID);
    }
}