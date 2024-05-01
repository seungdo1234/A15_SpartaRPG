using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Scripts
{
    public class Quest
    {
        public string QuestName { get; set; }
        public int CurrentProgress { get; set; }
        public int TotalProgress {  get; set; }
        public int? RewardGold { get; set; }
        public Item? RewardItem { get; set; }
        public string QuestContent { get; set; } 

    }
}
