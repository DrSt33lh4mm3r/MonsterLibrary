using System;
using MonsterLibrary.Monsters.Model.Helpers;

namespace MonsterLibrary.Monsters.Model
{
    public record Stat
    {
        public int value { get; init; }
        public int modifier { get; init; }

        public override string ToString()
        {
            return value + ModifierFormater.FormatModifier(modifier, true);
        }
    }
}