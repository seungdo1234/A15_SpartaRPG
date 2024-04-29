
namespace TextRPG
{
    public class RestScreen :Screen
    {
        private int restGold = 500;

        public void RestScreenOn()
        {
            Console.Clear();

            while (true)
            {
                RestText();
                MyActionText();

                // 1, 2, 3만 입력 받을 수 있게 함 
                if (int.TryParse(Console.ReadLine(), out int input) && input >=0 && input <= 1)
                {

                    Console.Clear();
                    
                    if(input == 0)
                    {
                        return;
                    }
                    else
                    {
                        if (gm.Player.Gold >= restGold)
                        {
                            if (gm.Player.Health >= gm.Player.MaxHealth)
                            {
                                Console.WriteLine("\n이미 최대 체력입니다 !\n");
                            }
                            else
                            {
                                gm.Player.RecoveryHealth(gm.Player.MaxHealth);
                                gm.Player.Gold -= restGold;
                                Console.WriteLine("\n휴식을 완료했습니다.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nGold 가 부족합니다.\n");
                        }
                    }

                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다! 숫자를 다시 입력하세요.\n");
                }

            }
        }

        private void RestText()
        {
            Console.WriteLine();

            Console.WriteLine("휴식하기");
            Console.WriteLine($"{restGold} G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {gm.Player.Gold} G)");

            Console.WriteLine();

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
        }
    }
}
