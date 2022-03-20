using System;

namespace MonsterLibrary.Monsters.Model
{
    public record Speed
    {
        public string walk { get; init; }
        public string fly { get; init; }
        public string swim { get; init; }
        public string climb { get; init; }
        public string burrow { get; init; }
        public bool? canHover { get; init; }
    }
}