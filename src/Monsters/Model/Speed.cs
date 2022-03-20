using System;
using System.Collections.Generic;

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

        public override string ToString()
        {
            List<string> speeds = new List<string>();

            if (walk is not null)
            {
                speeds.Add(walk + "ft.");
            }

            if (fly is not null)
            {
                var flySpeed = "fly " + fly + "ft.";

                if (canHover is not null && canHover! == true) {
                    flySpeed = flySpeed + " (hover)";
                }

                speeds.Add(flySpeed);
            }

            if (swim is not null)
            {
                speeds.Add("swim " + swim + "ft.");
            }

            if (climb is not null)
            {
                speeds.Add("climb " + climb + "ft.");
            }

            if (burrow is not null)
            {
                speeds.Add("burrow " + burrow + "ft.");
            }

            Console.WriteLine(String.Join(", ", speeds.ToArray()));

            return String.Join(", ", speeds.ToArray());
        }
    }
}