
namespace TextRPG
{
    public class DeathSentence : Skill
    {
        public DeathSentence(int id) : base(id)
        {
        }

        public override string CastSkill(Unit caster, Unit target)
        {
            string result;
            int damage = caster.GetDamagePerHit();
            int activeRate = random.Next(1, 101);

            if (activeRate == 1)
            {
                damage = target.Health;
                result = target.OnDamagedDenyDef(damage);                

                return result + "길동무가 되었다...";
            }            

            return "아무일도 일어나지 않았습니다";
        }
    }
}
