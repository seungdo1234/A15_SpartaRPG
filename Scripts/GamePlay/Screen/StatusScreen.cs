
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
            if (gm.Player.EquipAtkItem != 0)
            {
                Console.Write($" (+{gm.Player.EquipAtkItem:F1})");
            }
            Console.WriteLine();

            Console.Write($"방어력 : {gm.Player.GetDefValue():F1}");
            if (gm.Player.EquipDefItem != 0)
            {
                Console.Write($" (+{gm.Player.EquipDefItem:F1})");
            }
            Console.WriteLine("\n");

            Console.WriteLine($"치명타 확률 : {gm.Player.CriticalChance}%");
            Console.WriteLine($"치명타 데미지 : {gm.Player.CriticalDamage * 100}%");
            Console.WriteLine($"회피율 : {gm.Player.AvoidChance}%");

            Console.WriteLine();

            Console.WriteLine("[스킬 정보]");

            for(int i = 0; i < gm.Player.Skills.Count; i++)
            {
                Skill skill = gm.Player.Skills[i];
                if(gm.Player.Phase > i)
                {
                    Console.WriteLine($"[{skill.Name}] 마나소모 : {skill.ManaCost} ");
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

            Console.WriteLine($"Gold : {gm.Player.Gold} G");

            Console.WriteLine("\n0. 나가기\n");            
        }
    }
}
