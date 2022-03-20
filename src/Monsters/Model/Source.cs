using System;

namespace MonsterLibrary.Monsters.Model
{
    public record Source
    {
        public string book { get; init; }
        public int page { get; init; }
    }
}