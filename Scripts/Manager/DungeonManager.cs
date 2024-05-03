
namespace TextRPG
{

    public class DungeonManager : DungeonData
    {
        public EDungeonResultType resultType { get; set; }
        public EDungeonDifficulty dif {  get; set; }
        public RandomReward RandomReward { get; private set; }
        public DungeonManager()
        {
            EnemyDataManager.instance.Init();
            RandomReward = new RandomReward();
        }

        // 던전 보상 만드는 함수
        public Reward GetDungeonReward()
        {
            Reward reward = new Reward();

            // 난이도 별 랜덤 보상 로직 추가

            return reward;
        }

        /*
        public void DungeonLevelUp(EDungeonDifficulty dif)
        {
            CurrentDungeonLevel += (int)dif;
        }
        */

    }
}
