using System;

namespace MonsterLibrary.Monsters.Model
{
    public record Monster
    {
        public Guid? Id { get; init; }
        public string name { get; init; }
        public Source source { get; init; }
        public string type { get; init; }
        public string size { get; init; }
        public string alignment { get; init; }
        public string languages { get; init; }
        public string environment { get; init; }
        public StatBlock statBlock { get; init; }
        public AC[] ac { get; init; }
        public HP hp { get; init; }
        public string cr { get; init; }
        public int passivePerception { get; init; }
        public Speed speed { get; init; }
        public Skill[] skills { get; init; }
        public Trait[] traits { get; init; }
        public Action[] actions { get; init; }
        public Action[] reactions { get; init; }
        public Save[] saves { get; init; }
        public string senses { get; init; }
        public Immunities immunities { get; init; }
        public string resistances { get; init; }
        public string vulnerabilities { get; init; }
        public string token { get; init; }
        public Legendary legendary { get; init; }

    }
}