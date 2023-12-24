using System;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public string Level;
        public int Score;
        public int MonsterQuantity;
        public int CurrentLevels;

        public WorldData(string level)
        {
            Level = level;
            MonsterQuantity = 3;
            CurrentLevels = 1;
        }
    }
}