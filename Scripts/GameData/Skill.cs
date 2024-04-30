namespace TextRPG
{
    internal class Skill
    {
        public string Name { get; private set; }
        public int TargetCount {  get; private set; }
        public List<int> DamageList { get; private set; }
        public bool IsMultiTarget {  get; private set; }
        public int ManaCost {  get; private set; } // 마나 소모량
        public string Content { get; private set; } // 스킬 설명
    }
}
