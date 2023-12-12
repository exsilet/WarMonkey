using UnityEngine;

namespace UI.Leaderboard
{
    public class LeaderboardElemetData
    {
        public int Place;
        public string Name;
        public int Result;
        
        public LeaderboardElemetData(int place, string name, int result)
        {
            Place = place;
            Name = name;
            Result = result;
        }
    }
}