

namespace TextRPG
{
    internal class EquipScreen : Screen
    {

        // 장비 장착
        public void EquipScreenOn()
        {
            Console.Clear();

            while (true)
            {
                EquipText ();
                MyActionText();

                // 0. 뒤로 가기  장비 번호 : 장착/ 장착 해제
                if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input <= dm.PlayerEquipItems.Count)
                {

                    if (input == 0) 
                    {
                        return;
                    }

                    EquipItem equipItem = dm.PlayerEquipItems[input - 1];
                    Equip(equipItem);

                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 숫자를 제대로 입력하세요. \n");
               }
            }
        }

        private void Equip(EquipItem equipItem) // 장비 장착 함수
        {
            equipItem.IsEquip = !equipItem.IsEquip; // 선택한 장비 장착

            if (equipItem.IsEquip) // 장비를 장착한 거라면
            {
                if (equipItem.EquipmenttType == EEquipItemType.WEAPON) // 무기일 때
                {
                    if(gm.Player.EquipAtkItem != null)
                    {
                        gm.Player.EquipAtkItem.IsEquip = false;
                    }
                    gm.Player.EquipAtkItem = equipItem;
                }
                else // 방어구도 똑같이 동작
                {
                    if (gm.Player.EquipDefItem != null)
                    {
                        gm.Player.EquipDefItem.IsEquip = false;
                    }
                    gm.Player.EquipDefItem = equipItem;
                }
            }
            else // 장착 해제
            {
                if (equipItem.EquipmenttType == EEquipItemType.WEAPON) // 장착 아이템, 장착 하고 있던 아이템 정보 초기화
                {
                    gm.Player.EquipAtkItem = null;
                }
                else
                {
                    gm.Player.EquipDefItem = null;
                }
            }   
        }

        // 장비 장착 텍스트 출력
        private void EquipText()
        {
            Console.WriteLine();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 장착할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < dm.PlayerEquipItems.Count; i++)
            {
                Console.Write($"- {i + 1} ");
                InventoryItemText(dm.PlayerEquipItems[i]);
            }

            Console.WriteLine();

            Console.WriteLine("0. 나가기");

            Console.WriteLine();

        }
    }
}
