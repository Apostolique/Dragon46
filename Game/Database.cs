using System;
using System.Collections.Generic;

namespace GameProject
{
    public enum CharacterType
    {
        // hero
        Warrior, Archer, Wizard, Cleric,
        // enemy
        Goblin,
    }

    public enum AbilityType
    {
        Defend
    }

    public enum StatusEffectType
    {
        Sleep, Stun, Poison, HealOverTime
    }

    public static class Database
    {
        private static Dictionary<AbilityType, Ability> _abilities;
        private static Dictionary<CharacterType, List<AbilityType>> _castableAbilities;

        private static Dictionary<StatusEffectType, Type> _statusEffects;

        public static void Load()
        {
            _abilities = new Dictionary<AbilityType, Ability>()
            {
                {
                    AbilityType.Defend,
                    new Ability()
                },
            };

            _castableAbilities = new Dictionary<CharacterType, List<AbilityType>>()
            {
                {
                    CharacterType.Warrior,
                    new List<AbilityType>()
                    {
                        AbilityType.Defend
                    }
                },
            }; // CastableAbilities

            _statusEffects = new Dictionary<StatusEffectType, Type>()
            {
                { StatusEffectType.Poison, typeof(PoisonStatus) }
            };
        }

        public static Ability GetAbility(AbilityType type)
        {
            if (!_abilities.ContainsKey(type))
                throw new System.Exception("Ability not defined: " + type.ToString());

            return _abilities[type];
        }

        public static List<Ability> GetCharacterAbilities(CharacterType type)
        {
            if (!_castableAbilities.ContainsKey(type))
                throw new Exception("Castable abilities not defined: " + type.ToString());

            var abilities = new List<Ability>();

            foreach (var t in _castableAbilities[type])
            {
                abilities.Add(GetAbility(t));
            }

            return abilities;
        }

        public static Type GetStatusEffect(StatusEffectType type)
        {
            if (!_statusEffects.ContainsKey(type))
                throw new Exception("Status effect not defined: " + type.ToString());

            return _statusEffects[type];
        }
    }
}