
using Newtonsoft.Json;

namespace TextRPG
{ 
    public class Item // 아이템 정보가 담긴 클래스
    {
        public string ItemName { get; protected set; }
        public EItemRank ItemRank { get; protected set; }
        public string Desc { get; protected set; }
        public int Gold { get; protected set; }


        // 아이템 등급 색깔 별 출력
        public void GetItemRankName()
        {

            switch (ItemRank)
            {
                case EItemRank.COMMON:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[일반]");
                    break;
                case EItemRank.RARE:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("[희귀]");
                    break;
                case EItemRank.EPIC:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("[영웅]");
                    break;
            }

            Console.ResetColor();
        }
    }
}