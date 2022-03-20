using System;

namespace MonsterLibrary.Monsters.Model
{
    public record AC
    {
        public int value { get; init; }
        public string note { get; init; }

        public override string ToString()
        {
            if (note is not null && note != "")
            {
                return value + " (" + note + ")";
            }
            else
            {
                return value.ToString();
            }           
        }
    }
}