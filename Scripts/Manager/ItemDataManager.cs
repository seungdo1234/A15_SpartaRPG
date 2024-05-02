namespace TextRPG
{
    public class ItemDataManager // 데이터들을 관리하는 클래스
    {
        public static ItemDataManager instance = new ItemDataManager();

        // 장비 아이템 DB
        public List<EquipItem> EquipItemDB { get; private set; }
        // 플레이어, 상점 아이템 리스트
        public List<EquipItem> PlayerEquipItems {  get; private set; }
        public List<EquipItem> ShopEquipItems { get; private set; }

        private Random rand = new Random();
        private int ShopEquipItemCount = 6; // 상점 장비 아이템 갯수

        // 플레이어, 상점 아이템 초기화
        public void Init()
        {
            PlayerEquipItems = new List<EquipItem>();
            ShopEquipItems = new List<EquipItem>();
            ShopItemReset();
        }

        public void SetItems( List<EquipItem> shopItems, List<EquipItem> playerItems)
        {
            PlayerEquipItems= playerItems;
            ShopEquipItems= shopItems;
        }

        public void SetItemDB(List<EquipItem> itemDB) 
        {
            EquipItemDB = itemDB;
        }

        // 5.1 J => 상점 아이템 초기화 로직 구현
        public void ShopItemReset() // 상점 아이템 초기화
        {
            List<EquipItem> equipItems = EquipItemDB.FindAll(obj => obj.ItemRank != EItemRank.EPIC);

            ShopEquipItems.Clear();
            for (int i = 0; i < ShopEquipItemCount; i++)
            {
                ShopEquipItems.Add(GetRandomEquipItem(equipItems));
            }
        }
        // 리스트에서 아이템 랜덤으로 하나 리턴
        public EquipItem GetRandomEquipItem(List<EquipItem> equipItemList) 
        {
            int itemnum = rand.Next(0, equipItemList.Count);

            return equipItemList[itemnum]; 
        }

        // 4.30 J => 플레이어 아이템 추가 로직 변경 (상점 구매, 던전 드랍 아이템)
        // 아이템 구매
        public void BuyShopItem(EquipItem equipItem)
        {
            PlayerEquipItems.Add(equipItem); // 구매한 아이템 플레이어 아이템 리스트에 추가
            GameManager.instance.Player.Gold -= equipItem.Gold; // 골드 --
            equipItem.IsSell = true; // 팔렸다 표시
        }
        // 4.30 J => 플레이어 아이템 추가 로직 변경 (상점 구매, 던전 드랍 아이템)
        public void DungeonDropItem(EquipItem equipItem) // 플레이어 아이템 추가
        {
            PlayerEquipItems.Add(equipItem);
        }


        // 아이템 판매 시 플레이어 보유 아이템에서 제거
        public void RemovePlayerItem(EquipItem equipItem) {

            PlayerEquipItems.Remove(equipItem);
        }
    }
}
