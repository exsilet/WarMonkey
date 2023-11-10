using System;

namespace Data
{
    [Serializable]
    public class State
    {
        public int GameLevel;
        public int Difficult;
        public int CurrentHP;
        public int MaxHP;
        public void ResetHP() => CurrentHP = MaxHP;

        public State()
        {
            GameLevel = 0;
            Difficult = 1;
            CurrentHP = 3;
            MaxHP = 3;
        }
    }
}