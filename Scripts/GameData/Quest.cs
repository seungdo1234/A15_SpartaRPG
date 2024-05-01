using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Scripts
{
    public class Quest
    {
        string QuestName { get; set; }

        //몬스터 처치, 처치한 몬스터 종류, 특정 스테이지 도달, 게임 클리어(보상?) 
        string QuestType { get; set; } 
    }
}
