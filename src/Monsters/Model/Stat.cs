using System;

namespace MonsterLibrary.Monsters.Model
{
    public record Stat
    {
        public int value { get; init; }
        public int modifier { get; init; }
    }
}