using System;

namespace MonsterLibrary.Monsters.Model
{
    public record HP
    {
        public int average { get; init; }
        public string formula { get; init; }

        public override string ToString()
        {
            return average + " (" + formula + ")";
        }
    }
}