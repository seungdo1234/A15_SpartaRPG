
using Newtonsoft.Json;

namespace TextRPG
{ 
    public class Item // 아이템 정보가 담긴 클래스
    {
        public string ItemName { get; protected set; }
        public EItemRank ItemRank { get; protected set; }
        public string Desc { get; protected set; }
        public int Gold { get; protected set; }


    }
}