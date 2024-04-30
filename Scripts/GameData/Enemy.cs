
namespace TextRPG
{
    public class Enemy : Unit
    {

        public Enemy()
        {
            Name = "더미";
            Level = 1;
            Atk = 10;
            Def = 5;
            MaxHealth = 100;
            Health = MaxHealth;
            AvoidChance = 10;
            CriticalChance = 16;
            CriticalDamage = 1.6f;
        }


        public void EnemyOnDamage(int damage)
        {
            this.Health -= damage - (int)this.Def;
        }

    }
}
