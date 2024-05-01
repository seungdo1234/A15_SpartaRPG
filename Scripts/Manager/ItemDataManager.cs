namespace TextRPG
{
    public class ItemDataManager // 데이터들을 관리하는 클래스
    {
        public static ItemDataManager instance = new ItemDataManager();

        public List<Item> ItemDB { get; private set; }
        // 플레이어, 상점 아이템 리스트
        public List<Item> PlayerItems {  get; private set; }
        public List<Item> ShopItems { get; private set; }

        // 플레이어, 상점 아이템 초기화
        public void Init()
        {
            PlayerItems = new List<Item>();
            ShopItems = new List<Item>();
            ItemDB = new List<Item>();
        }

        public void SetItems( List<Item> shopItems, List<Item> playerItems)
        {
            PlayerItems= playerItems;
            ShopItems= shopItems;
        }

        public void SetItemDB(List<Item> itemDB)
        {
            ItemDB = itemDB;
        }


        // 4.30 J => 플레이어 아이템 추가 로직 변경 (상점 구매, 던전 드랍 아이템)
        // 아이템 구매
        public void BuyShopItem(Item item)
        {
            PlayerItems.Add(item); // 구매한 아이템 플레이어 아이템 리스트에 추가
            GameManager.instance.Player.Gold -= item.Gold; // 골드 --
            item.IsSell = true; // 팔렸다 표시
        }
        // 4.30 J => 플레이어 아이템 추가 로직 변경 (상점 구매, 던전 드랍 아이템)
        public void DungeonDropItem(Item item) // 플레이어 아이템 추가
        {
            PlayerItems.Add(item);
        }


        // 아이템 판매 시 플레이어 보유 아이템에서 제거
        public void RemovePlayerItem(Item item) {

            PlayerItems.Remove(item);
        }
    }
}
