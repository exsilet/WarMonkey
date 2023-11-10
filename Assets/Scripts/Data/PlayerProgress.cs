using System;
using Infrastructure.StaticData.Enemy;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State HeroState;
        public WorldData WorldData;
        public int CurrentSoul;
        public KillData KillData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new State();
            KillData = new KillData();
        }
    }
}