using System;

namespace GameProject
{
    public class Ability
    {
        /// <summary>
        /// In milliseconds
        /// </summary>
        public int CastTime { get; set; }
        public int Damage { get; set; }

        public Ability() { }

        public void Apply(Character target)
        {
            target.ApplyDamage(Damage);
        }
    }
}
