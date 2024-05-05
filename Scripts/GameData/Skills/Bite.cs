
namespace TextRPG
{
    public class Bite : Skill
    {
        public Bite(int id) : base(id)
        {
        }

        public override string CastSkill(Unit caster, Unit target)
        {
            string result;
            string? critStr = caster.IsCriticalHit();
            float critRate = critStr != null ? caster.CriticalDamage : 1f;
            float skillRate = 1.3f;
            int damage = caster.GetDamagePerHit();

            damage = Convert.ToInt32(Math.Round(damage * skillRate * critRate));
            result = target.OnDamaged(damage);
            result += critStr;

            return result;
        }
    }
}
