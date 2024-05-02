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
            
            Console.Clear();

            Console.WriteLine("Quest!!\n");
            Console.WriteLine("스토리 퀘스트 >");

            while (true)
            {
                bool IsEnterKey = false;
                

                do
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("좌우 방향키로 페이지를 넘기고, 엔터 키로 내용을 상세히 확인하세요!");

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
                            if (page == 5)
                            {
                                Console.WriteLine("마지막 페이지입니다!");
                                page--;
                                Thread.Sleep(1000);
                                Console.SetCursorPosition(0, 6);
                                Console.WriteLine("                     ");
                            }
                            break;
                        default:
                            break;
                    }

                    Console.SetCursorPosition(0, 2);
                    switch(page)
                    {
                        case 1:
                            Console.WriteLine("스토리 퀘스트 >     ");
                            break;
                        case 2:
                            Console.WriteLine("< 반복 퀘스트 >       ");
                            break;
                        case 3:
                            Console.WriteLine("< 몬스터 도감 >       ");
                            break;
                        case 4:
                            Console.WriteLine("< 수집한 장비 업적     ");
                            break;
                        default:
                            break;
                    }

                } while (!IsEnterKey);
                

                switch (page)
                {
                    case 1:
                        StoryQuestText();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
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
                Console.WriteLine("-보상-");

                if (q.RewardItem != null)
                {
                    Console.WriteLine(q.RewardItem.ItemName);
                }
                if (q.RewardGold != null)
                {
                    Console.WriteLine($"{q.RewardGold} G");
                }

                Console.WriteLine("\n");

                if (q.CurrentProgress == q.TotalProgress)
                {
                    Console.WriteLine("1. 보상 받기");
                }

                Console.WriteLine("0. 돌아가기");

                Console.ReadKey();
            }
            
        }
    }
}
