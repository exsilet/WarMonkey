namespace UI.Leaderboards
{
    public class LeaderboardElementData
    {
        public int Place;
        public string Name;
        public int Result;
        
        public LeaderboardElementData(int place, string name, int result)
        {
            Place = place;
            Name = name;
            Result = result;
        }
    }
}