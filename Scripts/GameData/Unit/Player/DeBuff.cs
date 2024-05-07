
namespace TextRPG
{
    public abstract class DeBuff
    {        
        public string Name { get; protected set; }
        public int Duration { get; protected set; }
        public int MaxDuration { get; protected set; }
        public string Caster { get; protected set; }

        public DeBuff(string name, int duration, string caster)
        {            
            Name = name;
            MaxDuration = duration;
            Duration = MaxDuration;
            Caster = caster;
        }

        public void Reapply()
        {
            Duration = MaxDuration;
        }
        public abstract void ActiveDebuff(Unit target);        
    }
}
