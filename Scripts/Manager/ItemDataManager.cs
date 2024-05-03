namespace TextRPG
{
    public class ItemDataManager // 데이터들을 관리하는 클래스
    {
        public static ItemDataManager instance = new ItemDataManager();

        // 장비 아이템 DB
        public List<EquipItem> EquipItemDB { get; private set; }
        // 플레이어, 상점 아이템 리스트
        public List<EquipItem> ShopEquipItems { get; private set; }

        private Random rand = new Random();
        private int ShopEquipItemCount = 6; // 상점 장비 아이템 갯수

        // 플레이어, 상점 아이템 초기화
        public void Init()
        {
            ShopEquipItems = new List<EquipItem>();
            ShopItemReset();
        }

        public void SetItems( List<EquipItem> shopItems)
        {
            ShopEquipItems= shopItems;
        }

        public void SetShopItems(List<EquipItem> itemDB) 
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

        // 4.30 J => 플레이어 아이템 추가 로직 변경 (상점 구매, 던전 드랍 아이템) / 5.3 J => 플레이어 보유 아이템 리스트 리팩토링
        // 아이템 구매
        public void BuyShopItem(EquipItem equipItem)
        {
            GameManager.instance.Player.AddEquipItem(equipItem); // 구매한 아이템 플레이어 아이템 리스트에 추가
            GameManager.instance.Player.Gold -= equipItem.Gold; // 골드 --
            equipItem.IsSell = true; // 팔렸다 표시
        }
    }
}
