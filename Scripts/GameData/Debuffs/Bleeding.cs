
namespace TextRPG
{
    public class Bleeding : DeBuff
    {
        public Bleeding(string name, int duration, string caster) : base(name, duration, caster)
        {
        }

        public override void ActiveDebuff(Unit target)
        {
            Duration--;
            Console.WriteLine($"{Caster}의 {Name}에 의한 출혈 {target.OnDamagedDenyDef(3)} [남은 턴: {Duration}]");
            Thread.Sleep(1500);
            if(Duration == 0)
            {
                target.DebuffActiveHandler -= ActiveDebuff;
                target.DeBuffs.Remove(this);
            }
        }
    }
}
