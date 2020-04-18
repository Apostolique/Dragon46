namespace GameProject
{
    public class AbilityEffectResult
    {
        public Buff Buff;
        public StatusEffect StatusEffect;
    }

    public class AbilityEffect
    {
        public int Duration;

        // offensive vs target
        public int LifeLeech { get; set; }
        public int HitStun { get; set; }

        // defensive on target
        public int AddTargetArmour { get; set; }
        public int TargetHeal { get; set; }

        // offensive on caster
        public int SelfDamage { get; set; }

        // defensive on caster
        public int AddCasterArmour { get; set; }

        public AbilityEffectResult Apply(Character caster, Character target, Ability ability)
        {
            var result = new AbilityEffectResult();

            if (HitStun > 0)
                target.CastingAbility?.AddTimeRemaining(HitStun);

            if (LifeLeech > 0)
                caster.ApplyHeal(LifeLeech);

            if (SelfDamage > 0)
                caster.ApplyDamage(SelfDamage);

            if (TargetHeal > 0)
                target.ApplyHeal(TargetHeal);

            if (AddTargetArmour > 0)
                result.Buff = new Buff(target, Duration) { AddArmour = AddTargetArmour };

            if (AddCasterArmour > 0)
                result.Buff = new Buff(caster, Duration) { AddArmour = AddCasterArmour };

            return result;
        }
    }
}