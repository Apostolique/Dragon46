using System;
using System.Collections.Generic;

namespace GameProject
{
    public enum CharacterType
    {
        // hero
        Warrior, Archer, Wizard, Cleric,
        // enemy
        GoblinMinion, GoblinBrute, GoblinShaman,
        Dragon
    }

    public enum AbilityType
    {
        // player abilities
        Heal,
        StoneSkin,
        Silence,
        // party abilities
        Defend,
        BasicAttack,
        // enemy abilities
        BasicEnemyAttack,
    }

    public enum StatusEffectType
    {
        Sleep, Stun, Poison, HealOverTime
    }

    public enum DamageType
    {
        Pure,
        Physical,
        Magical
    }

    public static class Database
    {
        private static Dictionary<AbilityType, Ability> _abilities;
        private static Dictionary<CharacterType, List<AbilityType>> _castableAbilities;
        private static Dictionary<StatusEffectType, Type> _statusEffects;
        private static Dictionary<CharacterType, EnemyType> _enemies;
        private static Dictionary<CharacterType, HeroType> _heroes;

        public static void Load()
        {
            _abilities = new Dictionary<AbilityType, Ability>()
            {
                {
                    AbilityType.Defend,
                    new Ability()
                    {
                        Name = "Defend",
                        CastTime = 2000,
                        TargetSelf = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                AddCasterArmour = 5,
                                Duration = 5000
                            }
                        }
                    }
                },
                {
                    AbilityType.BasicAttack,
                    new Ability()
                    {
                        Name = "Basic Attack",
                        CastTime = 2500,
                        Damage = 10,
                        DamageType = DamageType.Physical,
                    }
                },
            };

            _castableAbilities = new Dictionary<CharacterType, List<AbilityType>>()
            {
                {
                    CharacterType.Warrior,
                    new List<AbilityType>()
                    {
                        AbilityType.Defend,
                        AbilityType.BasicAttack
                    }
                },
                {
                    CharacterType.Archer,
                    new List<AbilityType>()
                    {
                        AbilityType.Defend,
                        AbilityType.BasicAttack
                    }
                },
                {
                    CharacterType.Wizard,
                    new List<AbilityType>()
                    {
                        AbilityType.Defend
                    }
                },
                {
                    CharacterType.Cleric,
                    new List<AbilityType>()
                    {
                        AbilityType.Defend
                    }
                },
                {
                    CharacterType.GoblinMinion,
                    new List<AbilityType>()
                    {
                        AbilityType.Defend
                    }
                },
                {
                    CharacterType.GoblinBrute,
                    new List<AbilityType>()
                    {
                        AbilityType.Defend
                    }
                },
                {
                    CharacterType.GoblinShaman,
                    new List<AbilityType>()
                    {
                        AbilityType.Defend
                    }
                },
                {
                    CharacterType.Dragon,
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

            _enemies = new Dictionary<CharacterType, EnemyType>()
            {
                {
                    CharacterType.GoblinMinion,
                    new EnemyType(
                        type: CharacterType.GoblinMinion, maxHP: 100, name: "Goblin Minion", baseArmour: 5, baseMagicResistance: 0, difficultyScore: 1,
                        baseStrength: 0, baseIntelligence: 0
                    )
                },
                {
                    CharacterType.GoblinBrute,
                    new EnemyType(
                        type: CharacterType.GoblinBrute, maxHP: 200, name: "Goblin Brute", baseArmour: 10, baseMagicResistance: 0, difficultyScore: 2,
                        baseStrength: 0, baseIntelligence: 0
                    )
                },
                {
                    CharacterType.GoblinShaman,
                    new EnemyType(
                        type: CharacterType.GoblinShaman, maxHP: 150, name: "Goblin Shaman", baseArmour: 5, baseMagicResistance: 0, difficultyScore: 2,
                        baseStrength: 0, baseIntelligence: 0
                    )
                },
                {
                    CharacterType.Dragon,
                    new EnemyType(
                        type: CharacterType.Dragon, maxHP: 2000, name: "Dragon", baseArmour: 20, baseMagicResistance: 0, difficultyScore: 10,
                        baseStrength: 0, baseIntelligence: 0
                    )
                },
            };

            _heroes = new Dictionary<CharacterType, HeroType>()
            {
                {
                    CharacterType.Cleric,
                    new HeroType(
                        type: CharacterType.Cleric, maxHP: 100, name: "Cleric", baseArmour: 5, baseMagicResistance: 0,
                        baseStrength: 0, baseIntelligence: 0
                    )
                },
                {
                    CharacterType.Wizard,
                    new HeroType(
                        type: CharacterType.Wizard, maxHP: 90, name: "Wizard", baseArmour: 5, baseMagicResistance: 0,
                        baseStrength: 0, baseIntelligence: 0
                    )
                },
                {
                    CharacterType.Archer,
                    new HeroType(
                        type: CharacterType.Archer, maxHP: 120, name: "Archer", baseArmour: 10, baseMagicResistance: 0,
                        baseStrength: 0, baseIntelligence: 0
                    )
                },
                {
                    CharacterType.Warrior,
                    new HeroType(
                        type: CharacterType.Warrior, maxHP: 200, name: "Warrior", baseArmour: 20, baseMagicResistance: 0,
                        baseStrength: 0, baseIntelligence: 0
                    )
                }
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

        public static EnemyType GetEnemy(CharacterType type)
        {
            if (!_enemies.ContainsKey(type))
                throw new Exception("Enemy not defined: " + type.ToString());

            return _enemies[type];
        }

        public static EnemyType GetRandomEnemy(Random rng)
        {
            var index = rng.Next(0, _enemies.Count);
            var counter = 0;

            foreach (var enemy in _enemies)
            {
                if (counter == index)
                    return enemy.Value;
                counter++;
            }

            return null;
        }

        public static HeroType GetHero(CharacterType type)
        {
            if (!_heroes.ContainsKey(type))
                throw new Exception("Hero not defined: " + type.ToString());

            return _heroes[type];
        }
    }
}