using System;

namespace MonsterLibrary.Monsters.Model
{
    public record Skill
    {
        public string name { get; init; }
        public int modifier { get; init; }
    }
}