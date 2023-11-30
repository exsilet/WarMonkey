using System;

namespace Data
{
    [Serializable]
    public class State
    {
        public int GameLevel;
        public int Difficult;
        public float CurrentHP;
        public float MaxHP;
        public void ResetHP() => CurrentHP = MaxHP;

        public State()
        {
            GameLevel = 0;
            Difficult = 1;
            CurrentHP = 5;
            MaxHP = 5;
        }
    }
}