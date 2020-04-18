using System.Collections.Generic;

namespace GameProject
{
    public class Ability
    {
        /// <summary>
        /// In milliseconds
        /// </summary>
        public int CastTime { get; set; }
        public int Damage { get; set; }
        public string Name { get; set; }

        protected List<AbilityEffect> _onHitEffects;

        public Ability() { }

        public void Apply(Character caster, Character target)
        {
            target.ApplyDamage(Damage);

            for (var i = 0; i < _onHitEffects.Count; i++)
                _onHitEffects[i].Apply(caster, target, this);
        }
    }
}
