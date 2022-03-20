using System;

namespace MonsterLibrary.Monsters.Model
{
    public record StatBlock
    {
        public Stat str { get; init; }
        public Stat dex { get; init; }
        public Stat con { get; init; }
        public Stat inte { get; init; } // cant name the int stat int because int is a rserved word -.-
        public Stat wis { get; init; }
        public Stat cha { get; init; }
    }
}