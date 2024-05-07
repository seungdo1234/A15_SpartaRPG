
namespace TextRPG
{
    public class CrisisEvasion : Skill
    {
        public CrisisEvasion(int id) : base(id)
        {
        }

        public override string CastSkill(Unit caster, Unit target)
        {
            string result;
            string? critStr = caster.IsCriticalHit();
            float critRate = critStr != null ? caster.CriticalDamage : 1f;
            float skillRate = (caster.MaxHealth - caster.Health) > 0 ? (caster.MaxHealth - caster.Health) * 0.01f : 1f;
            skillRate += 1.5f;
            int damage = caster.GetDamagePerHit();
                        
            damage = Convert.ToInt32(Math.Round(damage * skillRate * critRate));
            result = target.OnDamaged(damage);
            result += critStr + $"{target.Name}는 이제 눈앞이 혼미해집니다. ";

            DeBuff? debuff = target.DeBuffs?.Find(x => x.Caster == caster.Name); // 스킬이 아닌 시전자로 검색
            if (debuff == null) // 타겟의 디버프목록에 없을 시 새 디버프 추가 
            {
                target.SetCrowdControl(ECrowdControlType.STUN);
                debuff = new Stun(Name, 2, caster.Name);
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
