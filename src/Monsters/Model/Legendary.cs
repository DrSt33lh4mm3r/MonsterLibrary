using System;

namespace MonsterLibrary.Monsters.Model
{
    public record Legendary
    {
        public string header { get; init; }
        public Action[] actions { get; init; }
    }
}