
namespace TextRPG
{
    public class Burning : DeBuff
    {
        public Burning(string name, int duration, string caster) : base(name, duration, caster)
        {
        }

        public override void ActiveDebuff(Unit target)
        {
            Duration--;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Caster}에 의해 당신은 불타고 있다. ");
            Console.ResetColor();
            Console.Write($"{target.OnDamagedDenyDef(4)} ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[남은 턴: {Duration}]");
            Console.ResetColor();

            Thread.Sleep(1500);
            if (Duration == 0)
            {
                target.DebuffActiveHandler -= ActiveDebuff;
                target.DeBuffs.Remove(this);
            }
        }
    }
}
