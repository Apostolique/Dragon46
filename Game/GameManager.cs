using System;
using System.Collections.Generic;

namespace GameProject
{
    public class GameManager
    {
        protected Random _rng;
        protected List<Encounter> _encounters;
        protected int _currentEncounter = -1;
        public Encounter CurrentEncounter { get => _currentEncounter >= _encounters.Count ? null : _encounters[_currentEncounter]; }

        public void Load(Random rng)
        {
            _rng = rng;
            _encounters = new List<Encounter>();

            for (var i = 0; i < Encounter.Waves.Count; i++)
                CreateEncounter(i);
        }

        public Encounter NextEncounter()
        {
            _currentEncounter++;
            return CurrentEncounter;
        }

        public Encounter CreateEncounter(int index)
        {
            var encounter = new Encounter(index);
            _encounters.Add(encounter);
            return encounter;
        }
    }
}