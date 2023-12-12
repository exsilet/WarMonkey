using System;

namespace Data
{
    [Serializable]
    public class State
    {
        public int GameLevel;
        public float CurrentHP;
        public float MaxHP;
        public int CurrentNumberSpawners;
        public void ResetHP() => CurrentHP = MaxHP;

        public State()
        {
            GameLevel = 1;
            CurrentNumberSpawners = 3;
        }
    }
}