namespace MonsterLibrary.Monsters.Model
{
    public record MonsterSpellEntry
    {
        public string name { get; init; }
        public string intro { get; init; }
        public MonsterSpellLevel[] spellLevels { get; init; }
        public string outro { get; init; }
    }
}