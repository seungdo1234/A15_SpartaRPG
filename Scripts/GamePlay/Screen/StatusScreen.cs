
namespace TextRPG
{
    public class StatusScreen :Screen
    {
        
        // 상태 보기 
        public override void ScreenOn()
        {
            while (true)
            {   
                Console.Clear();
                StatusText();
                MyActionText();

                // 0 입력 시 나가기
                if (int.TryParse(Console.ReadLine(), out int input) && input == 0)
                {   
                    return;
                }
                else
                {
                    //Console.WriteLine("잘못된 입력입니다! 로비로 돌아갈려면 0번을 입력하세요. \n");
                    SystemMessageText(EMessageType.ERROR);
                }
            }

        }

        // 상태 보기 텍스트 출력
        private void StatusText()
        {
            Console.WriteLine();

            PrintTitle("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.Write("Lv.");
            PrintNotice(gm.Player.Level.ToString());
            Console.Write("  ( ");
            PrintName(gm.Player.Name);
            Console.WriteLine(" )");

            Console.Write("직업 ( ");
            PrintNotice(gm.Player.GetPlayerClass(gm.Player.ePlayerClass));
            Console.WriteLine(" )");

            Console.WriteLine();

            Console.Write($"체력 : ");
            PrintNotice(gm.Player.Health.ToString());
            Console.Write("/");
            PrintNotice(gm.Player.MaxHealth.ToString());
            Console.WriteLine();
            Console.Write($"마나 : ");
            PrintNotice(gm.Player.Mana.ToString());
            Console.Write("/");
            PrintNotice(gm.Player.MaxMana.ToString());
            Console.WriteLine("\n");

            // 현재 장착중인 장비 능력치 적용
            Console.Write($"공격력 : ");
            PrintNotice(gm.Player.GetAtkValue().ToString("N1"));
            Console.ForegroundColor = ConsoleColor.Green;
            if (gm.Player.EquipAtkItem != 0)
            {
                Console.Write($" (+{gm.Player.EquipAtkItem:F1})");
            }
            Console.ResetColor();
            Console.WriteLine();

            Console.Write($"방어력 : ");
            PrintNotice(gm.Player.GetDefValue().ToString("N1"));
            Console.ForegroundColor = ConsoleColor.Green;
            if (gm.Player.EquipAtkItem != 0)
                if (gm.Player.EquipDefItem != 0)
            {
                Console.Write($" (+{gm.Player.EquipDefItem:F1})");
            }
            Console.ResetColor();
            Console.WriteLine("\n");

            Console.Write($"치명타 확률 : ");
            PrintNotice(gm.Player.CriticalChance.ToString());
            Console.WriteLine("%");

            Console.Write($"치명타 데미지 : ");
            PrintNotice((gm.Player.CriticalDamage * 100).ToString());
            Console.WriteLine("%");

            Console.Write($"회피율 : ");
            PrintNotice(gm.Player.AvoidChance.ToString());
            Console.WriteLine("%");

            Console.WriteLine();

            PrintTitle("스킬 정보");

            for(int i = 0; i < gm.Player.Skills.Count; i++)
            {
                Skill skill = gm.Player.Skills[i];
                if(gm.Player.Phase > i)
                {
                    Console.Write("[");
                    PrintName(skill.Name);
                    Console.WriteLine($"] 마나소모 : {skill.ManaCost} ");
                    Console.WriteLine($"\t{skill.Content}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[{skill.Name}] 마나소모 : {skill.ManaCost} ");
                    Console.Write("\t[잠김]\t해금레벨 : ");
                    switch (i)
                    {
                        case 0:
                            Console.WriteLine("2");
                            break;
                        case 1:
                            Console.WriteLine("5");
                            break;
                        case 2:
                            Console.WriteLine("7");
                            break;
                    }                    
                }
                Console.ResetColor();
            }

            Console.Write($"Gold : ");
            PrintNotice(gm.Player.Gold.ToString());
            Console.WriteLine(" G");

            Console.WriteLine("\n0. 나가기\n");            
        }
    }
}
