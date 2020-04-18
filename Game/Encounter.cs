using System.Collections.Generic;

namespace GameProject
{
    public class Encounter
    {
        protected int _difficulty;
        public int Difficulty { get => _difficulty; }

        public List<Enemy> Enemies;
    }
}