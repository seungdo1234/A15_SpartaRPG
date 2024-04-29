
namespace TextRPG
{
    public class GameManager
    {
        public static GameManager instance = new GameManager();

        private Player player;
        public Player Player { get => player; set { player = value; } }

        private DungeonData dungeonData;
        public DungeonData DungeonData { get => dungeonData; }


        private SaveSystem saveSystem;
        public SaveSystem SaveSystem { get => saveSystem; }

        public void Init() // 객체 초기화
        {
            dungeonData = new DungeonData();
            saveSystem = new SaveSystem();
        }
    }
}
