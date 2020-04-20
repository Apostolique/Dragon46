using System.Collections.Generic;

namespace GameProject
{
    public class Encounter
    {
        public static List<List<Enemy>> Waves = new List<List<Enemy>>()
        {
            //new List<Enemy>()
            //{
            //    new Enemy(Database.GetEnemy(CharacterType.Dragon)),
            //},
            new List<Enemy>()
            {
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
            },
            new List<Enemy>()
            {
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinBrute)),
            },
            new List<Enemy>()
            {
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinBrute)),
            },
            new List<Enemy>()
            {
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinShaman)),
            },
            new List<Enemy>()
            {
                new Enemy(Database.GetEnemy(CharacterType.GoblinBrute)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinBrute)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinShaman)),
            },
            new List<Enemy>()
            {
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinBrute)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinShaman)),
            },
            new List<Enemy>()
            {
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinBrute)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinShaman)),
            },
            new List<Enemy>()
            {
                new Enemy(Database.GetEnemy(CharacterType.GoblinMinion)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinBrute)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinBrute)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinShaman)),
            },
            new List<Enemy>()
            {
                new Enemy(Database.GetEnemy(CharacterType.GoblinBrute)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinBrute)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinShaman)),
                new Enemy(Database.GetEnemy(CharacterType.GoblinShaman)),
            },
            new List<Enemy>()
            {
                new Enemy(Database.GetEnemy(CharacterType.Dragon))
            },
        };

        protected int _difficulty;
        public int Difficulty { get => _difficulty; }

        public List<Enemy> Enemies;

        public Encounter(int index)
        {
            Enemies = Waves[index];
        }
    }
}