
namespace TextRPG
{
    public class DungeonManager : DungeonData
    {
        
        public struct Reward
        {
            public Item dungeonResultItem;
            public int gold;
            public int exp;
        }


        public Reward GetDungeonReward(DungeonDifficulty dif)
        {
            Reward reward = new Reward();



            return reward;
        }
    }
}
