
namespace TextRPG
{
    public class DemonClaw : Skill
    {
        public DemonClaw(int id) : base(id)
        {
        }

        public override string CastSkill(Unit caster, Unit target)
        {
            string result;
            float skillDamage = target.Health * 0.1f;           

            int damage = Convert.ToInt32(Math.Round(skillDamage));
            result = target.OnDamagedDenyDef(damage);            

            return result;
        }
    }
}

