namespace TextRPG
{
    public class SkillData
    {
        private static int idCount = 0;
        public int ID {  get; private set; }
        public string Name { get; private set; }
        public int MaxTargetCount {  get; private set; }        
        public bool IsMultiTarget {  get; private set; }
        public int ManaCost {  get; private set; } // 마나 소모량
        public string Content { get; private set; } // 스킬 설명

        public SkillData(string name, int maxTarget, bool isMulti, int manaCost, string content)
        {
            ID = idCount++;
            Name = name;
            MaxTargetCount = maxTarget;            
            IsMultiTarget = isMulti;
            ManaCost = manaCost;
            Content = content;
        }
    }
}
