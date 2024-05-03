
using Newtonsoft.Json;
using TextRPG.Scripts;

namespace TextRPG
{

    struct PlayerDatas // Json에 저장할 플레이어 데이터 구조체
    { 
        public Player player;
        public List<EquipItem> shopItems;
        public List<(string QuestType, int QuestNumber, int CurrentProgress)> questSave;

        public PlayerDatas()
        {
            player = GameManager.instance.Player;
            shopItems = ItemDataManager.instance.ShopEquipItems;
        }
    }

    public class SaveSystem
    {
        // Exe 파일이 들어 있는 경로 저장
        private string path = AppDomain.CurrentDomain.BaseDirectory;

        public void Save() //24.05.03 데이터 로드 방식 변경 - C
        {
            string filePath = $"{path}\\SaveSlot\\PlayerData.json";

            // PlayerDatas 구조체 생성
            PlayerDatas data = new PlayerDatas(); 

            // 저장된 플레이어의 정보를 직렬화
            string jsonData = JsonConvert.SerializeObject(data);

            // 플레이어 데이터 Json 파일 저장
            using (var js = new StreamWriter(filePath))
            {
                js.WriteLine(jsonData.ToString());
                js.Close();
            }
        }

        public bool CheckLoad()
        {
            string saveDirectory = $"{path}\\SaveSlot";
            string playerFile = Path.Combine(saveDirectory, "PlayerData.json");

            if (!Directory.Exists(saveDirectory)) //폴더 유무
            {
                Directory.CreateDirectory(saveDirectory); 
                return false;
            }
            else if (!File.Exists(playerFile)) //플레이어 파일 유무
            {
                return false;
            }
            {
                return true;
            }
        }

        //24.05.03 데이터 로드 방식 변경 - C
        public Player Load(string name)
        {
            string equipItemJsonFilePath = Path.Combine(path, @"..\..\..\SaveData\EquipItemData.json");
            string consumableJsonFilePath = Path.Combine(path, @"..\..\..\SaveData\ConsumableItemData.json");
            string saveDirectory = $"{path}\\SaveSlot";
            string playerFile = Path.Combine(saveDirectory, "PlayerData.json");

            // 전체 아이템 데이터 불러오기
            string equipItemJsonData = File.ReadAllText(equipItemJsonFilePath); 
            string consumableItemJsonData = File.ReadAllText(consumableJsonFilePath); 

            // DB 적용
            ItemDataManager.instance.SetItemDB(JsonConvert.DeserializeObject<List<EquipItem>>(equipItemJsonData), 
                JsonConvert.DeserializeObject<List<ConsumableItem>>(consumableItemJsonData));

            if (name == "") // 불러오기 승낙한 경우
            {
                // 데이터가 존재한다면 해당 데이터를 읽어옴
                string playerJsonData = File.ReadAllText(playerFile);

                // PlayerDatas 구조체로 역직렬화해서 텍스트를 데이터로 변환
                PlayerDatas playerData = JsonConvert.DeserializeObject<PlayerDatas>(playerJsonData);

                // 불러온 플레이어 정보 저장
                ItemDataManager.instance.SetShopItems(playerData.shopItems);
                return playerData.player;

            }
            else // 불러오기 거절 또는 새로운 플레이어인 경우
            {
                // 기본 데이터로 초기화
                ItemDataManager.instance.Init();

                //새 플레이어 객체 생성
                Player newPlayer = new Player(name);

                //QuestSave 초기화
                GameManager.instance.QuestManager.QuestSave[0] = (QuestType: "story", QuestNumber: 0, CurrentProgress: -1);
                GameManager.instance.QuestManager.QuestSave[1] = (QuestType: "monster", QuestNumber: 0, CurrentProgress: -1);
                GameManager.instance.QuestManager.QuestSave[2] = (QuestType: "storyLog", QuestNumber: -1, CurrentProgress: 0);
                GameManager.instance.QuestManager.QuestSave[3] = (QuestType: "monsterLog", QuestNumber: -1, CurrentProgress: 0);
                return newPlayer;
            }
        }
    }
}
