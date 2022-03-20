using System;

namespace MonsterLibrary.Monsters.Model
{
    public record Save
    {
        public string name { get; init; }
        public int modifier { get; init; }
    }
}