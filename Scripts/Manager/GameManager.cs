
namespace TextRPG
{
    public class GameManager
    {
        public static GameManager instance = new GameManager();

        public Player Player { get;  set; }

        public DungeonManager Dungeon { get; private set; }

        public SaveSystem SaveSystem  { get; private set; }

        public void Init() // 객체 초기화
        {
            Dungeon = new DungeonManager();
            SaveSystem = new SaveSystem();
        }
    }
}
