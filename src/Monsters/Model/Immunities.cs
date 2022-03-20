using System;

namespace MonsterLibrary.Monsters.Model
{
    public record Immunities
    {
        public string damage { get; init; }
        public string condition { get; init; }
    }
}