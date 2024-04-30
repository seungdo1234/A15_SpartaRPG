
namespace TextRPG
{
    public struct Reward
    {
        public Item dungeonRewardItem;
        public int gold;
        public int exp;

        public Reward()
        {
            dungeonRewardItem = new Item("낡은 검", ItemTypes.WEAPON, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
            gold = 500;
            exp = 5;
        }
    }

    public struct MonsterEncount
    {
        public List<Enemy> spawnMonsters;

        public MonsterEncount(int CurrentDungeonLevel)
        {
            EnemyDataManager.instance.Init();

            spawnMonsters = EnemyDataManager.instance.GetSpawnMonsters(CurrentDungeonLevel);
        }
    }

    public class DungeonManager : DungeonData
    {
        protected Random random = new Random();
        
        //랜덤 몬스터리스트 가져오는 함수
        public static List<Enemy> GetMonsterEncount(int CurrentDungeonLevel)
        {
            MonsterEncount monsterEncount = new MonsterEncount(CurrentDungeonLevel);

            return monsterEncount.spawnMonsters;
        }

        public Reward GetDungeonReward(DungeonDifficulty dif)
        {
            Reward reward = new Reward();



            return reward;
        }
    }
}
