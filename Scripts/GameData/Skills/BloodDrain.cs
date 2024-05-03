
namespace TextRPG
{
    public class BloodDrain : Skill
    {
        public BloodDrain(int id) : base(id)
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
            caster.OnDamagedDenyDef(-damage);
            result += critStr;

            return result;
        }
    }
}
