using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class CrisisEvasion : SkillData
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
            target.OnDamaged(damage);
            result = $"[데미지 {damage}] " + critRate;

            return result;
        }
    }
}
