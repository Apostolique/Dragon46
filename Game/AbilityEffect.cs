namespace GameProject
{
    public class AbilityEffect
    {
        // vs target
        protected int _lifeLeech;
        protected int _hitStun;

        public void Apply(Character caster, Character target, Ability ability)
        {
            if (_hitStun > 0)
                target.CastingAbility?.AddTimeRemaining(_hitStun);

            if (_lifeLeech > 0)
                caster.ApplyHeal(_lifeLeech);
        }
    }
}