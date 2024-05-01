
namespace TextRPG
{
    public struct Reward
    {
        public Item dungeonRewardItem;
        public int gold;
        public int exp;

        public Reward()
        {
            dungeonRewardItem = new Item("낡은 검", EEquipItemType.WEAPON, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
            gold = 500;
            exp = 5;
        }
    }

    public struct MonsterEncount
    {
        public List<Enemy> spawnMonsters;

        public MonsterEncount(int CurrentDungeonLevel)
        {
            spawnMonsters = EnemyDataManager.instance.GetSpawnMonsters(CurrentDungeonLevel);
        }
    }

    public class DungeonManager : DungeonData
    {
        protected Random random = new Random();
        
        public DungeonManager()
        {
            EnemyDataManager.instance.Init();
        }

        //랜덤 몬스터리스트 가져오는 함수
        public List<Enemy> GetMonsterEncount(int CurrentDungeonLevel)
        {
            MonsterEncount monsterEncount = new MonsterEncount(CurrentDungeonLevel);

            return monsterEncount.spawnMonsters;
        }

        // 던전 보상 만드는 함수
        public Reward GetDungeonReward(EDungeonDifficulty dif)
        {
            Reward reward = new Reward();

            // 난이도 별 랜덤 보상 로직 추가

            return reward;
        }

        public void DungeonLevelUp(EDungeonDifficulty dif)
        {
            CurrentDungeonLevel += (int)dif;
        }


    }
}
