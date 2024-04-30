using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class EnemySkill //일반몹: 필살기. //boss: 스킬.
    {
        private string Name { get; }
        private float Damage { get; }
        private string Content { get; } //간단한 스킬설명

        public EnemySkill(string name, int level, float baseDamage, string content)
        {
            Name = name;
            Damage = baseDamage * ((10 + level)/10);
            Content = content;
        }

        public void Activate(Unit target)
        {

        }
    }
}
