
namespace TextRPG
{
    public class Assassination : SkillData
    {
        public Assassination(int id) : base(id)
        {
        }

        public override string CastSkill(Unit caster, Unit target)
        {
            string result;
            string? critStr = caster.IsCriticalHit();
            float critRate = critStr != null ? caster.CriticalDamage : 1f;
            float skillRate = (target.Health / target.MaxHealth) > 0.2f ? 1.5f : 0;
            int damage = caster.GetDamagePerHit();

            if (skillRate == 0)
            {
                damage = target.Health;
                target.OnDamaged(damage);
                result = $"[데미지 {damage}] ";

                return result;
            }

            damage = Convert.ToInt32(Math.Round(damage * skillRate * critRate));
            target.OnDamaged(damage);
            result = $"[데미지 {damage}] " + critRate;

            return result;
        }
    }
}

