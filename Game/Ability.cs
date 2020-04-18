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

        public List<AbilityEffect> OnCastEffects = new List<AbilityEffect>();
        public List<AbilityEffect> OnHitEffects = new List<AbilityEffect>();

        public Ability() { }

        public void Cast(Character caster, Character target)
        {
            for (var i = 0; i < OnCastEffects.Count; i++)
            {
                var result = OnCastEffects[i].Apply(caster, target, this);
                HandleEffectResult(result);
            }
        }

        public void Apply(Character caster, Character target)
        {
            if (Damage > 0)
                target.ApplyDamage(Damage);

            for (var i = 0; i < OnHitEffects.Count; i++)
            {
                var result = OnHitEffects[i].Apply(caster, target, this);
                HandleEffectResult(result);
            }
        }

        public void HandleEffectResult(AbilityEffectResult result)
        {
            if (result.Buff != null)
                result.Buff.Owner.ApplyBuff(result.Buff);
            if (result.StatusEffect != null)
                result.StatusEffect.Owner.ApplyStatusEffect(result.StatusEffect);
        }
    }
}
