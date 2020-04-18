namespace GameProject
{
    public class AbilityEffect
    {
        protected int _lifeLeech;
        protected int _hitStun;

        public void Apply(Character caster, Character target, Ability ability)
        {
            target.CastingAbility?.AddTimeRemaining(_hitStun);

            if (_lifeLeech > 0)
                caster.ApplyHeal(_lifeLeech);
        }
    }
}