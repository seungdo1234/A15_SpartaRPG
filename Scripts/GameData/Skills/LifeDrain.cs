
namespace TextRPG
{
    public class LifeDrain : Skill
    {
        public LifeDrain(int id) : base(id)
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
            caster.RecoveryHealth(damage / 2); // 체력의 절반흡수
            result += critStr;

            return result;
        }
    }
}
