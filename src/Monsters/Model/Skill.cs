using System;
using MonsterLibrary.Monsters.Model.Helpers;

namespace MonsterLibrary.Monsters.Model
{
    public record Skill
    {
        public string name { get; init; }
        public int modifier { get; init; }

        public override string ToString()
        {
            return name + ModifierFormater.FormatModifier(modifier);
        }
    }
}