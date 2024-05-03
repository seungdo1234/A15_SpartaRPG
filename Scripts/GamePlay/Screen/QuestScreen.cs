using System.Collections.Generic;
using TextRPG.Scripts;

namespace TextRPG
{
    public class QuestScreen : Screen
    {
        public override void ScreenOn()
        {
            int page = 1; //퀘스트 창 페이지.
            bool isExit = false;
            
            while (!isExit)
            {
                bool IsEnterKey = false;
                
                do
                {
                    Console.Clear();

                    Console.WriteLine("Quest!!\n");
                    switch (page)
                    {
                        case 1:
                            Console.WriteLine("스토리 퀘스트 >      ");
                            break;
                        case 2:
                            Console.WriteLine("< 사냥 퀘스트 >      ");
                            break;
                        case 3:
                            Console.WriteLine("< 완료된 퀘스트      ");
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine("\n좌우 방향키로 페이지를 넘기고, 엔터 키로 내용을 상세히 확인하세요!");
                    Console.WriteLine("만약 메뉴로 돌아가길 원한다면, 0키를 누르세요!");


                    var key = Console.ReadKey(true).Key;

                    switch(key)
                    {
                        case ConsoleKey.Enter:
                            IsEnterKey = true;
                            break;
                        case ConsoleKey.LeftArrow:
                            page--;
                            if (page == 0)
                            {
                                Console.WriteLine("\n\n첫번째 페이지입니다!");
                                page++;
                                Thread.Sleep(500);
                                Console.SetCursorPosition(0, 6);
                                Console.WriteLine("                     ");
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            page++;
                            if (page == 4) //페이지 증가시 바꾸기
                            {
                                Console.WriteLine("\n\n마지막 페이지입니다!");
                                page--;
                                Thread.Sleep(500);
                                Console.SetCursorPosition(0, 6);
                                Console.WriteLine("                     ");
                            }
                            break;
                        case ConsoleKey.D0: //알파벳 위의 0
                            isExit = true;
                            IsEnterKey = true;
                            page = 0;
                            break;
                        case ConsoleKey.NumPad0: //숫자패드 0
                            isExit = true;
                            IsEnterKey = true;
                            page = 0;
                            break;
                        default:
                            SystemMessageText(EMessageType.ERROR);
                            break;
                    }
                   
                    Console.SetCursorPosition(0, 2);

                } while (!IsEnterKey);
                

                switch (page)
                {
                    case 1:
                        StoryQuestText();
                        break;
                    case 2:
                        MonsterQuestText();
                        break;
                    case 3:
                        QuestArchiveText();
                        break;
                    default:
                        break;
                }
            }
        }

        private void StoryQuestText()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Story Quest!!\n");

                Quest q = gm.QuestManager.GetCurrentStoryQuest();

                Console.WriteLine(q.QuestName + "\n");
                Console.WriteLine();
                Console.WriteLine(q.QuestContent + "\n");
                Console.WriteLine($"-스테이지 {q.TotalProgress} 깨기");
                Console.WriteLine("\n-보상-");

                if (q.RewardItem != null)
                {
                    Console.WriteLine(q.RewardItem.ItemName);
                }
                if (q.RewardGold != 0)
                {
                    Console.WriteLine($"{q.RewardGold} G");
                }

                Console.WriteLine();

                if (q.CurrentProgress >= q.TotalProgress)
                {
                    Console.WriteLine("1. 보상 받기");
                }else
                {
                    Console.WriteLine();
                }

                Console.WriteLine("0. 돌아가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                if (int.TryParse(Console.ReadLine(), out int input) && (input == 0 || input == 1))
                {
                    if (input == 1)
                    {
                        if (q.CurrentProgress >= q.TotalProgress)
                        {
                            gm.Player.Gold += q.RewardGold;
                            Console.WriteLine($"\n보상을 수령했습니다. +{q.RewardGold} G");
                            var oldQ = gm.QuestManager.QuestSave[0];
                            var newQ = (oldQ.QuestType, ++oldQ.QuestNumber, oldQ.CurrentProgress);
                            gm.QuestManager.QuestSave[0] = newQ;
                        }
                        else
                        {
                            Console.WriteLine("\n\n얌체시군요!");
                            Thread.Sleep(750);
                        }
                    }
                    else
                    {
                        Console.Write("\n\n퀘스트 선택창으로 돌아갑니다.");
                        Thread.Sleep(750);
                        break;
                    }
                }else
                {
                    SystemMessageText(EMessageType.ERROR);
                }
            }
        }

        private void MonsterQuestText()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Monster Quest!!\n");

                Quest q = gm.QuestManager.GetCurrentMonsterQuest();

                Console.WriteLine(q.QuestName + "\n");
                Console.WriteLine();
                Console.WriteLine(q.QuestContent + "\n");

                if (q.CurrentProgress >= 0)
                {
                    switch (gm.QuestManager.QuestSave[1].QuestNumber)
                    {
                        case 0:
                            Console.WriteLine($"-몬스터 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 1:
                            Console.WriteLine($"-사나운 토끼 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 2:
                            Console.WriteLine($"-노을의 늑대개 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 3:
                            Console.WriteLine($"-강을 건넌 사람 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 4:
                            Console.WriteLine($"-흐느끼는 유령 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 5:
                            Console.WriteLine($"-늘어나는 그림자 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 6:
                            Console.WriteLine($"-노래하는 물고기 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 7:
                            Console.WriteLine($"-흰머리 호랑이 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 8:
                            Console.WriteLine($"-외로운 불귀신 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 9:
                            Console.WriteLine($"-독수리 사자 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 10:
                            Console.WriteLine($"-녹안의 악마 {q.TotalProgress}마리 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        case 11:
                            Console.WriteLine($"-BOSS 휘몰아치는 강철이 처치 ({q.CurrentProgress}/{q.TotalProgress})");
                            break;
                        default:
                            break;
                    }
                }else
                {
                    Console.WriteLine();
                }

                Console.WriteLine("\n-보상-");

                if (q.RewardItem != null)
                {
                    Console.WriteLine(q.RewardItem.ItemName);
                }
                if (q.RewardGold != 0)
                {
                    Console.WriteLine($"{q.RewardGold} G");
                }

                Console.WriteLine("");

                if (q.CurrentProgress < 0)
                {
                    Console.WriteLine("\n1. 수락하기");
                }
                else if (q.CurrentProgress >= q.TotalProgress)
                {
                    Console.WriteLine("\n1. 보상 받기");
                }else
                {
                    Console.WriteLine("\n(진행중)");
                }

                Console.WriteLine("0. 돌아가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                if (int.TryParse(Console.ReadLine(), out int input) && (input == 0 || input == 1))
                {
                    if (input == 1)
                    {
                        if (q.CurrentProgress >= q.TotalProgress)
                        {
                            gm.Player.Gold += q.RewardGold;
                            Console.WriteLine($"보상을 수령했습니다. +{q.RewardGold} G");
                            var oldQ = gm.QuestManager.QuestSave[1];
                            var newQ = (oldQ.QuestType, ++oldQ.QuestNumber, CurrentProgress: -1);
                            gm.QuestManager.QuestSave[1] = newQ;
                            Thread.Sleep(750);
                        }
                        else if (q.CurrentProgress < 0)
                        {
                            var oldQ = gm.QuestManager.QuestSave[1];
                            var newQ = (oldQ.QuestType, oldQ.QuestNumber, CurrentProgress: 0);
                            gm.QuestManager.QuestSave[1] = newQ;
                        }
                        else
                        {
                            Console.WriteLine("\n\n...뭘 바라시는 거죠?");
                            Thread.Sleep(750);
                        }
                    }
                    else
                    {
                        Console.Write("\n\n퀘스트 선택창으로 돌아갑니다.");
                        Thread.Sleep(500);
                        break;
                    }
                }
                else
                {
                    SystemMessageText(EMessageType.ERROR);
                }
            }

        }

        public void QuestArchiveText()
        {
            int page = 1;
            int pageIndex;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("~ Quest Archive ~\n");

                List<Quest> EndQ = new List<Quest>();
                
                EndQ.AddRange(gm.QuestManager.GetStoryLog());
                EndQ.AddRange(gm.QuestManager.GetEnemyLog());

                if (EndQ.Count > 9)
                {
                    pageIndex = 9 * (page - 1);
                    for (int i = pageIndex; i < pageIndex + 9; i++)
                    {
                        if (EndQ[i] == null)
                        {
                            break;
                        }else
                        {
                            Console.Write((i + 1) + ". ");
                            Console.WriteLine(EndQ[i].QuestName);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < EndQ.Count; i++)
                    {
                        Console.Write((i + 1) + ". ");
                        Console.WriteLine(EndQ[i].QuestName);
                    }
                }
                Console.WriteLine();

                var key = Console.ReadKey(true).Key;
                if (EndQ.Count > 9)
                {
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            page--;
                            if (page == 0)
                            {
                                Console.WriteLine("\n\n첫번째 페이지입니다!");
                                page++;
                                Thread.Sleep(500);
                                Console.SetCursorPosition(0, 6);
                                Console.WriteLine("                     ");
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            page++;
                            if (page ) //페이지 증가시 바꾸기
                            {
                                Console.WriteLine("\n\n마지막 페이지입니다!");
                                page--;
                                Thread.Sleep(500);
                                Console.SetCursorPosition(0, 6);
                                Console.WriteLine("                     ");
                            }
                            break;
                        case ConsoleKey.D0: //알파벳 위의 0
                            break;
                        case ConsoleKey.NumPad0: //숫자패드 0
                            break;
                        default:
                            SystemMessageText(EMessageType.ERROR);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("0. 돌아가기\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");

                    switch (key)
                    {
                        case ConsoleKey.D0: //알파벳 위의 0
                            break;
                        case ConsoleKey.NumPad0: //숫자패드 0
                            break;
                        default:
                            SystemMessageText(EMessageType.ERROR);
                            break;
                    }
                }
                
                Console.Write(">>");
            }
        }
    }
}
