
namespace TextRPG
{
    public struct Reward
    {
        public EquipItem dungeonRewardItem; // 던전 보상 추가 예정
        public int gold;
        public int exp;

        public Reward()
        {
            // 추후 난이도 별로 장비아이템이 바뀌도록 변경 해야함
            dungeonRewardItem = ItemDataManager.instance.GetRandomEquipItem(ItemDataManager.instance.EquipItemDB);
            gold = 500;
            exp = 5;
        }
    }

    public class DungeonManager : DungeonData
    {
        protected Random random = new Random();

        public EDungeonResultType resultType { get; set; }
        public EDungeonDifficulty dif {  get; set; }

        public DungeonManager()
        {
            EnemyDataManager.instance.Init();
        }

        // 던전 보상 만드는 함수
        public Reward GetDungeonReward(EDungeonDifficulty dif)
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
