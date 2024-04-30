
namespace TextRPG
{
    public struct Reward
    {
        public Item dungeonResultItem;
        public int gold;
        public int exp;

        public Reward()
        {
            dungeonResultItem = new Item("낡은 검", ItemTypes.WEAPON, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
            gold = 500;
            exp = 5;
        }
    }
    public class DungeonManager : DungeonData
    {
        protected Random random = new Random();


        // 던전 보상 만드는 함수
        public Reward GetDungeonReward(DungeonDifficulty dif)
        {
            Reward reward = new Reward();

            

            return reward;
        }

        public void DungeonLevelUp(DungeonDifficulty dif)
        {
            CurrentDungeonLevel += (int)dif;
        }


    }
}
