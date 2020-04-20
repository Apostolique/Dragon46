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
        None,

        // player abilities
        HealingLight, // burst heal with long cast time
        Regeneration, // heal over time with shorter cast time
        Barrier, // add physical and magic defence
        Silence, // stop enemy abilities
        Distract, // slow down current ability timer

        // other abilities
        WarriorSword,
        WarriorShieldBash,
        WarriorDefend,

        ArrowShot,
        PoisonArrow,
        SilenceArrow,
        StunArrow,

        WizardStaff,
        WizardBarrier,
        WizardFireball,
        WizardStun,

        MinionSpear,
        MinionRegen,

        BruteSmash,
        BruteStun,
        BruteDefend,
        BruteRegen,

        ShamanStaff,
        ShamanFireball,
        ShamanHeal,
        ShamanPoison,

        DragonFire,
        DragonStun,
        DragonHeal,
        DragonRoar,
    }

    public enum StatusEffectType
    {
        Silence, Poison, HealOverTime
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
                // player abilities
                {
                    AbilityType.HealingLight,
                    new Ability()
                    {
                        AbilityType = AbilityType.HealingLight,
                        Name = "Healing Light",
                        CastTime = 5000,
                        TargetFriendly = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                TargetHeal = 50
                            }
                        }
                    }
                },
                {
                    AbilityType.Regeneration,
                    new Ability()
                    {
                        AbilityType = AbilityType.Regeneration,
                        Name = "Regeneration",
                        CastTime = 2500,
                        TargetFriendly = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                HealOverTime = 5
                            }
                        }
                    }
                },
                {
                    AbilityType.Barrier,
                    new Ability()
                    {
                        AbilityType = AbilityType.Barrier,
                        Name = "Barrier",
                        CastTime = 2500,
                        TargetFriendly = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                AddTargetArmour = 40,
                                AddTargetMagicResistance = 40
                            }
                        }
                    }
                },
                {
                    AbilityType.Silence,
                    new Ability()
                    {
                        AbilityType = AbilityType.Silence,
                        Name = "Silence",
                        CastTime = 3500,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                Silence = 2000
                            }
                        }
                    }
                },
                {
                    AbilityType.Distract,
                    new Ability()
                    {
                        AbilityType = AbilityType.Distract,
                        Name = "Distract",
                        CastTime = 2000,
                        Damage = 10,
                        DamageType = DamageType.Magical,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                HitStun = 1000
                            }
                        }
                    }
                },

                // warrior
                {
                    AbilityType.WarriorSword,
                    new Ability()
                    {
                        AbilityType = AbilityType.WarriorSword,
                        Name = "Sword",
                        CastTime = 2000,
                        DamageType = DamageType.Physical,
                        Damage = 15,
                    }
                },
                {
                    AbilityType.WarriorShieldBash,
                    new Ability()
                    {
                        AbilityType = AbilityType.WarriorShieldBash,
                        Name = "Shield Bash",
                        CastTime = 3500,
                        DamageType = DamageType.Physical,
                        Damage = 5,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                HitStun = 1000
                            }
                        }
                    }
                },
                {
                    AbilityType.WarriorDefend,
                    new Ability()
                    {
                        AbilityType = AbilityType.WarriorDefend,
                        Name = "Defend",
                        CastTime = 2000,
                        TargetSelf = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                AddTargetArmour = 25
                            }
                        }
                    }
                },

                // archer
                {
                    AbilityType.ArrowShot,
                    new Ability()
                    {
                        AbilityType = AbilityType.ArrowShot,
                        Name = "Arrow Shot",
                        CastTime = 2000,
                        DamageType = DamageType.Physical,
                        Damage = 15,
                    }
                },
                {
                    AbilityType.PoisonArrow,
                    new Ability()
                    {
                        AbilityType = AbilityType.PoisonArrow,
                        Name = "Poison Arrow",
                        CastTime = 2500,
                        Damage = 5,
                        DamageType = DamageType.Magical,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                Poison = 5
                            }
                        }
                    }
                },
                {
                    AbilityType.SilenceArrow,
                    new Ability()
                    {
                        AbilityType = AbilityType.SilenceArrow,
                        Name = "Silence Arrow",
                        CastTime = 3500,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                Silence = 1000
                            }
                        }
                    }
                },
                {
                    AbilityType.StunArrow,
                    new Ability()
                    {
                        AbilityType = AbilityType.StunArrow,
                        Name = "Stun Arrow",
                        CastTime = 3500,
                        DamageType = DamageType.Physical,
                        Damage = 5,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                HitStun = 1000
                            }
                        }
                    }
                },

                // wizard
                {
                    AbilityType.WizardStaff,
                    new Ability()
                    {
                        AbilityType = AbilityType.WizardStaff,
                        Name = "Staff",
                        CastTime = 2000,
                        DamageType = DamageType.Physical,
                        Damage = 10,
                    }
                },
                {
                    AbilityType.WizardBarrier,
                    new Ability()
                    {
                        AbilityType = AbilityType.WizardBarrier,
                        Name = "Barrier",
                        CastTime = 3000,
                        TargetFriendly = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                AddTargetArmour = 20,
                                AddTargetMagicResistance = 20,
                            }
                        }
                    }
                },
                {
                    AbilityType.WizardFireball,
                    new Ability()
                    {
                        AbilityType = AbilityType.WizardFireball,
                        Name = "Fireball",
                        CastTime = 3500,
                        DamageType = DamageType.Magical,
                        Damage = 25,
                    }
                },
                {
                    AbilityType.WizardStun,
                    new Ability()
                    {
                        AbilityType = AbilityType.WizardStun,
                        Name = "Stunning Light",
                        CastTime = 3500,
                        DamageType = DamageType.Magical,
                        Damage = 5,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                HitStun = 1000
                            }
                        }
                    }
                },

                // minion
                {
                    AbilityType.MinionSpear,
                    new Ability()
                    {
                        AbilityType = AbilityType.MinionSpear,
                        Name = "Spear",
                        CastTime = 2500,
                        DamageType = DamageType.Physical,
                        Damage = 10,
                    }
                },
                {
                    AbilityType.MinionRegen,
                    new Ability()
                    {
                        AbilityType = AbilityType.MinionRegen,
                        Name = "Regenerate",
                        CastTime = 3500,
                        TargetSelf = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                HealOverTime = 2
                            }
                        }
                    }
                },

                // brute
                {
                    AbilityType.BruteSmash,
                    new Ability()
                    {
                        AbilityType = AbilityType.BruteSmash,
                        Name = "Smashing Blow",
                        CastTime = 2500,
                        DamageType = DamageType.Physical,
                        Damage = 20,
                    }
                },
                {
                    AbilityType.BruteStun,
                    new Ability()
                    {
                        AbilityType = AbilityType.BruteStun,
                        Name = "Stunning Blow",
                        CastTime = 3000,
                        DamageType = DamageType.Physical,
                        Damage = 5,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                HitStun = 1000,
                            }
                        }
                    }
                },
                {
                    AbilityType.BruteDefend,
                    new Ability()
                    {
                        AbilityType = AbilityType.BruteDefend,
                        Name = "Defend",
                        CastTime = 2000,
                        TargetSelf = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                AddTargetArmour = 20,
                            }
                        }
                    }
                },
                {
                    AbilityType.BruteRegen,
                    new Ability()
                    {
                        AbilityType = AbilityType.BruteRegen,
                        Name = "Regenerate",
                        CastTime = 3500,
                        TargetSelf = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                HealOverTime = 5
                            }
                        }
                    }
                },

                // shaman
                {
                    AbilityType.ShamanStaff,
                    new Ability()
                    {
                        AbilityType = AbilityType.ShamanStaff,
                        Name = "Staff Strike",
                        CastTime = 2000,
                        Damage = 15,
                        DamageType = DamageType.Physical,
                    }
                },
                {
                    AbilityType.ShamanFireball,
                    new Ability()
                    {
                        AbilityType = AbilityType.ShamanFireball,
                        Name = "Fireball",
                        CastTime = 3000,
                        DamageType = DamageType.Magical,
                        Damage = 25,
                    }
                },
                {
                    AbilityType.ShamanHeal,
                    new Ability()
                    {
                        AbilityType = AbilityType.ShamanHeal,
                        Name = "Blood Sacrifice",
                        CastTime = 3500,
                        TargetFriendly = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                SelfDamage = 20,
                                TargetHeal = 30,
                            }
                        }
                    }
                },
                {
                    AbilityType.ShamanPoison,
                    new Ability()
                    {
                        AbilityType = AbilityType.ShamanPoison,
                        Name = "Poison Cloud",
                        CastTime = 2500,
                        Damage = 5,
                        DamageType = DamageType.Magical,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                Poison = 5
                            }
                        }
                    }
                },

                // dragon
                {
                    AbilityType.DragonFire,
                    new Ability()
                    {
                        AbilityType = AbilityType.DragonFire,
                        Name = "Dragon Fire",
                        CastTime = 3000,
                        Damage = 35,
                        DamageType = DamageType.Magical,
                    }
                },
                {
                    AbilityType.DragonStun,
                    new Ability()
                    {
                        AbilityType = AbilityType.DragonStun,
                        Name = "Stunning Blow",
                        CastTime = 2000,
                        Damage = 15,
                        DamageType = DamageType.Physical,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                HitStun = 1000
                            }
                        }
                    }
                },
                {
                    AbilityType.DragonHeal,
                    new Ability()
                    {
                        AbilityType = AbilityType.DragonHeal,
                        Name = "Heal",
                        CastTime = 3500,
                        TargetSelf = true,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                TargetHeal = 25
                            }
                        }
                    }
                },
                {
                    AbilityType.DragonRoar,
                    new Ability()
                    {
                        AbilityType = AbilityType.DragonRoar,
                        Name = "Roar",
                        CastTime = 2500,
                        Damage = 5,
                        DamageType = DamageType.Physical,
                        OnHitEffects = new List<AbilityEffect>()
                        {
                            new AbilityEffect()
                            {
                                Silence = 1000
                            }
                        }
                    }
                },
            };

            _castableAbilities = new Dictionary<CharacterType, List<AbilityType>>()
            {
                {
                    CharacterType.Warrior,
                    new List<AbilityType>()
                    {
                        AbilityType.WarriorSword,
                        AbilityType.WarriorShieldBash,
                        AbilityType.WarriorDefend,
                    }
                },
                {
                    CharacterType.Archer,
                    new List<AbilityType>()
                    {
                        AbilityType.ArrowShot,
                        AbilityType.PoisonArrow,
                        AbilityType.SilenceArrow,
                        AbilityType.StunArrow,
                    }
                },
                {
                    CharacterType.Wizard,
                    new List<AbilityType>()
                    {
                        AbilityType.WizardStaff,
                        AbilityType.WizardBarrier,
                        AbilityType.WizardFireball,
                        AbilityType.WizardStun,
                    }
                },
                {
                    CharacterType.Cleric,
                    new List<AbilityType>()
                    {
                        AbilityType.HealingLight,
                        AbilityType.Regeneration,
                        AbilityType.Barrier,
                        AbilityType.Silence,
                        AbilityType.Distract,
                    }
                },
                {
                    CharacterType.GoblinMinion,
                    new List<AbilityType>()
                    {
                        AbilityType.MinionSpear,
                        AbilityType.MinionRegen,
                    }
                },
                {
                    CharacterType.GoblinBrute,
                    new List<AbilityType>()
                    {
                        AbilityType.BruteSmash,
                        AbilityType.BruteStun,
                        AbilityType.BruteDefend,
                        AbilityType.BruteRegen,
                    }
                },
                {
                    CharacterType.GoblinShaman,
                    new List<AbilityType>()
                    {
                        AbilityType.ShamanStaff,
                        AbilityType.ShamanFireball,
                        AbilityType.ShamanHeal,
                        AbilityType.ShamanPoison,
                    }
                },
                {
                    CharacterType.Dragon,
                    new List<AbilityType>()
                    {
                        AbilityType.DragonFire,
                        AbilityType.DragonStun,
                        AbilityType.DragonHeal,
                        AbilityType.DragonRoar,
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
                        type: CharacterType.GoblinMinion, maxHP: 100, name: "Goblin Minion", baseArmour: 5, baseMagicResistance: 5, difficultyScore: 1,
                        baseStrength: 10, baseIntelligence: 0
                    )
                },
                {
                    CharacterType.GoblinBrute,
                    new EnemyType(
                        type: CharacterType.GoblinBrute, maxHP: 200, name: "Goblin Brute", baseArmour: 10, baseMagicResistance: 10, difficultyScore: 2,
                        baseStrength: 20, baseIntelligence: 0
                    )
                },
                {
                    CharacterType.GoblinShaman,
                    new EnemyType(
                        type: CharacterType.GoblinShaman, maxHP: 150, name: "Goblin Shaman", baseArmour: 15, baseMagicResistance: 15, difficultyScore: 2,
                        baseStrength: 0, baseIntelligence: 20
                    )
                },
                {
                    CharacterType.Dragon,
                    new EnemyType(
                        type: CharacterType.Dragon, maxHP: 2000, name: "Dragon", baseArmour: 35, baseMagicResistance: 35, difficultyScore: 10,
                        baseStrength: 30, baseIntelligence: 30
                    )
                },
            };

            _heroes = new Dictionary<CharacterType, HeroType>()
            {
                {
                    CharacterType.Cleric,
                    new HeroType(
                        type: CharacterType.Cleric, maxHP: 100, name: "Cleric", baseArmour: 10, baseMagicResistance: 25,
                        baseStrength: 0, baseIntelligence: 25
                    )
                },
                {
                    CharacterType.Wizard,
                    new HeroType(
                        type: CharacterType.Wizard, maxHP: 90, name: "Wizard", baseArmour: 10, baseMagicResistance: 25,
                        baseStrength: 0, baseIntelligence: 25
                    )
                },
                {
                    CharacterType.Archer,
                    new HeroType(
                        type: CharacterType.Archer, maxHP: 120, name: "Archer", baseArmour: 15, baseMagicResistance: 15,
                        baseStrength: 20, baseIntelligence: 20
                    )
                },
                {
                    CharacterType.Warrior,
                    new HeroType(
                        type: CharacterType.Warrior, maxHP: 200, name: "Warrior", baseArmour: 25, baseMagicResistance: 10,
                        baseStrength: 25, baseIntelligence: 0
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