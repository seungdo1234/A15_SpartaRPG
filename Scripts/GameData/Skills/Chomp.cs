﻿
namespace TextRPG
{
    public class Chomp : Skill
    {
        public Chomp(int id) : base(id)
        {
        }       

        public override string CastSkill(Unit caster, Unit target)
        {
            string result;
            string? critStr = caster.IsCriticalHit();
            float critRate = critStr != null ? caster.CriticalDamage : 1f;
            float skillRate = 1.2f;
            int damage = caster.GetDamagePerHit();

            damage = Convert.ToInt32(Math.Round(damage * skillRate * critRate));
            result = target.OnDamaged(damage);
            result += critStr;

            // 05.05 W 디버프 연결부
            result += $"{target.Name}은 피를 흘리기 시작합니다. ";            

            DeBuff? debuff = target.DeBuffs?.Find(x => x.Caster ==  caster.Name); // 스킬이 아닌 시전자로 검색
            if( debuff == null ) // 타겟의 디버프목록에 없을 시 새 디버프 추가 
            {
                debuff = new Bleeding(Name, 2, caster.Name);
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
