using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Scripts;

namespace TextRPG
{
    public class QuestScreen : Screen
    {
        public void QuestScreenOn()
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
                            Console.WriteLine("스토리 퀘스트 >     ");
                            break;
                        case 2:
                            Console.WriteLine("< 사냥 퀘스트 >       ");
                            break;
                        case 3:
                            Console.WriteLine("< 퀘스트 아카이브       ");
                            break;
                        //case 4:
                        //    Console.WriteLine("< 수집한 장비 업적     ");
                        //    break;
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
                                Console.WriteLine("첫번째 페이지입니다!");
                                page++;
                                Thread.Sleep(1000);
                                Console.SetCursorPosition(0, 6);
                                Console.WriteLine("                     ");
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            page++;
                            if (page == 4) //페이지 증가시 바꾸기
                            {
                                Console.WriteLine("마지막 페이지입니다!");
                                page--;
                                Thread.Sleep(1000);
                                Console.SetCursorPosition(0, 6);
                                Console.WriteLine("                     ");
                            }
                            break;
                        case ConsoleKey.D0: //알파벳 위의 0
                            isExit = true;
                            IsEnterKey = true;
                            page = 0;
                            break;
                        case ConsoleKey.NumPad0:
                            isExit = true;
                            IsEnterKey = true;
                            page = 0;
                            break;
                        default:
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
                        break;
                    //case 4:
                    //    break;
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
                Console.WriteLine("-보상-");

                if (q.RewardItem != null)
                {
                    Console.WriteLine(q.RewardItem.ItemName);
                }
                if (q.RewardGold != 0)
                {
                    Console.WriteLine($"{q.RewardGold} G");
                }

                Console.WriteLine("\n");

                if (q.CurrentProgress >= q.TotalProgress)
                {
                    Console.WriteLine("1. 보상 받기");
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
                            gm.QuestManager.QuestSaver[0, 0]++;
                        }
                        else
                        {
                            Console.WriteLine("얌체시군요!");
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        Console.Write("퀘스트 선택창으로 돌아갑니다.");
                        Thread.Sleep(1000);
                        break;
                    }
                }else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
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
                    switch (gm.QuestManager.QuestSaver[1, 0])
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

                Console.WriteLine("\n");

                if (q.CurrentProgress < 0)
                {
                    Console.WriteLine("1. 수락하기");
                }
                else if (q.CurrentProgress >= q.TotalProgress)
                {
                    Console.WriteLine("1. 보상 받기");
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
                            gm.QuestManager.QuestSaver[1, 0]++;
                            gm.QuestManager.QuestSaver[1, 1] = -1;
                        }
                        else if (q.CurrentProgress < 0)
                        {
                            gm.QuestManager.QuestSaver[1, 1] = 0;
                        }
                        else
                        {
                            Console.WriteLine("...뭘 바라시는 거죠?");
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        Console.Write("퀘스트 선택창으로 돌아갑니다.");
                        Thread.Sleep(1000);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }

        }
    }
}
