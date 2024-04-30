namespace TextRPG
{
    public abstract class Skills
    {
        public List<SkillData> SkillList { get; private set; }

        public Skills()
        {
            SkillList = new List<SkillData>();
        }

        protected void AddSkill(SkillData skill)
        {
            SkillList.Add(skill);
        }

        /// <summary>
        /// 스킬 시전 시 Hit 당 데미지 리스트를 반환해주는 메소드
        /// </summary>
        /// <param name="id">스킬ID</param>
        /// <param name="unit">스킬을 시전하는 Unit</param>
        /// <param name="enemyCount">전장의 타겟 수</param>
        /// <returns>item1 return (int)damage, item2 return (bool)isCrit</returns>
        public abstract List<(int damage, bool isCrit)>? GetSkillDamages(int id, Unit unit, int enemyCount);
    }
}
