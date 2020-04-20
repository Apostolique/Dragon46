namespace GameProject
{
    public class AbilityEffectResult
    {
        public Buff Buff;
        public StatusEffect StatusEffect;
    }

    public class AbilityEffect
    {
        public int Duration = 5000;

        // offensive vs target
        public int LifeLeech { get; set; }
        public int HitStun { get; set; }
        public int Poison { get; set; }
        public int Silence { get; set; }

        // defensive on target
        public int AddTargetArmour { get; set; }
        public int AddTargetMagicResistance { get; set; }
        public int TargetHeal { get; set; }
        public int HealOverTime { get; set; }

        // offensive on caster
        public int SelfDamage { get; set; }

        // defensive on caster
        public int AddCasterArmour { get; set; }
        public int AddCasterMagicResistance { get; set; }

        public AbilityEffectResult Apply(Character caster, Character target, Ability ability)
        {
            var result = new AbilityEffectResult();

            if (HitStun > 0)
                target.CastingAbility?.AddTimeRemaining(HitStun);

            if (LifeLeech > 0)
                caster.ApplyHeal(LifeLeech);

            if (Poison > 0)
            {
                var poisonStatus = new PoisonStatus(target, Duration)
                {
                    DamagePerTick = Poison,
                };

                poisonStatus.Start(1000);

                target.ApplyStatusEffect(poisonStatus);
            }

            if (HealOverTime > 0)
            {
                var healStatus = new HealOverTimeStatus(target, Duration)
                {
                    HealPerTick = HealOverTime,
                };

                healStatus.Start(1000);

                target.ApplyStatusEffect(healStatus);
            }

            if (Silence > 0)
            {
                var silenceStatus = new SilenceStatus(target, Silence);
                target.ApplyStatusEffect(silenceStatus);
            }

            if (SelfDamage > 0)
                caster.ApplyDamage(DamageType.Pure, SelfDamage);

            if (TargetHeal > 0)
                target.ApplyHeal(TargetHeal);

            if (AddTargetArmour > 0)
                result.Buff = new Buff(target, Duration, ability.Name) { AddArmour = AddTargetArmour };

            if (AddCasterArmour > 0)
                result.Buff = new Buff(caster, Duration, ability.Name) { AddArmour = AddCasterArmour };

            if (AddTargetMagicResistance > 0)
                result.Buff = new Buff(target, Duration, ability.Name) { AddMagicResistance = AddTargetMagicResistance };

            if (AddCasterMagicResistance > 0)
                result.Buff = new Buff(caster, Duration, ability.Name) { AddMagicResistance = AddCasterMagicResistance };

            return result;
        }
    }
}