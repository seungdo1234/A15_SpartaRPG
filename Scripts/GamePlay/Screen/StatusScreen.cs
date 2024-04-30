
namespace TextRPG
{
    public class StatusScreen :Screen
    {
        
        // 상태 보기 
        public void StatusScreenOn()
        {
            Console.Clear();

            while (true)
            {
                StatusText();
                MyActionText();

                // 0 입력 시 나가기
                if (int.TryParse(Console.ReadLine(), out int input) && input == 0)
                {
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 로비로 돌아갈려면 0번을 입력하세요. \n");
                }
            }

        }

        // 상태 보기 텍스트 출력
        private void StatusText()
        {
            Console.WriteLine();

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv.{gm.Player.Level}  ( {gm.Player.Name} )");
            Console.WriteLine($"직업 ( {gm.Player.GetPlayerClass(gm.Player.ePlayerClass)} )");

            Console.WriteLine();

            Console.WriteLine($"체력 : {gm.Player.Health}/{gm.Player.MaxHealth}");
            Console.WriteLine($"마나 : {gm.Player.Mana}/{gm.Player.MaxMana}");

            Console.WriteLine();

            // 현재 장착중인 장비 능력치 적용
            Console.Write($"공격력 : {gm.Player.GetAtkValue():F1}");
            if (gm.Player.EquipAtkItem != null )
            {
                Console.Write($" (+{gm.Player.EquipAtkItem.Value:F1})");
            }
            Console.WriteLine();

            Console.Write($"방어력 : {gm.Player.GetDefValue():F1}");
            if (gm.Player.EquipDefItem != null)
            {
                Console.Write($" (+{gm.Player.EquipDefItem.Value:F1})");
            }
            Console.WriteLine("\n");

            Console.WriteLine($"치명타 확률 : {gm.Player.CriticalChance}%");
            Console.WriteLine($"치명타 데미지 : {gm.Player.CriticalDamage * 100}%");
            Console.WriteLine($"회피율 : {gm.Player.AvoidChance}%");

            Console.WriteLine();

            Console.WriteLine($"Gold : {gm.Player.Gold} G");

            Console.WriteLine("\n0. 나가기\n");
        }
    }
}
