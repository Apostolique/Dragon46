using System.Collections.Generic;

namespace GameProject
{
    public class Ability
    {
        public DamageType DamageType;

        /// <summary>
        /// In milliseconds
        /// </summary>
        public int CastTime { get; set; }
        public int Damage { get; set; }
        public string Name { get; set; }
        public bool TargetFriendly { get; set; }
        public bool TargetSelf { get; set; }

        public int CooldownDuration { get; set; } = 600;

        public List<AbilityEffect> OnCastEffects = new List<AbilityEffect>();
        public List<AbilityEffect> OnHitEffects = new List<AbilityEffect>();

        public Ability()
        {
            OnCastEffects = new List<AbilityEffect>();
            OnHitEffects = new List<AbilityEffect>();
        }

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
            {
                var modifiedDamage = Damage;

                if (DamageType == DamageType.Physical)
                {
                    var totalStrength = caster.BaseStrength + caster.AddedStrength;
                    modifiedDamage = (int)(Damage + (Damage * (totalStrength / 100f)));
                }
                else if (DamageType == DamageType.Magical)
                {
                    var totalIntelligence = caster.BaseIntelligence + caster.AddedIntelligence;
                    modifiedDamage = (int)(Damage + (Damage * (totalIntelligence / 100f)));
                }

                target.ApplyDamage(DamageType, modifiedDamage);
            }

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
