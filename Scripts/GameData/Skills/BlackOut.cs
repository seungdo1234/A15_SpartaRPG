
namespace TextRPG
{
    public class BlackOut : Skill
    {
        public BlackOut(int id) : base(id)
        {
        }

        public override string CastSkill(Unit caster, Unit target)
        {
            string result;
                       
            result = $"{target.Name}에 실명 ";

            DeBuff? debuff = target.DeBuffs?.Find(x => x.Caster == caster.Name); // 스킬이 아닌 시전자로 검색
            if (debuff == null) // 타겟의 디버프목록에 없을 시 새 디버프 추가 
            {
                target.SetCrowdControl(ECrowdControlType.BLIND);
                debuff = new Blind(Name, 1, caster.Name);
                target.DeBuffs.Add(debuff);
                target.DebuffActiveHandler += debuff.ActiveDebuff;
            }
            else // 이미 디버프가 있을 시 최대 지속시간(턴)으로 갱신
            {
                debuff.Reapply();
            }

            return result;
        }
    }
}
