using System;

namespace MonsterLibrary.Monsters.Model
{
    public record Action
    {
        public string name { get; init; }
        public string description { get; init; }
    }
}