namespace MonsterLibrary.Monsters.Model
{
    public record LegendaryGroup
    {
        public ListItem[] lairActions { get; init; }
        public ListItem[] regionalEffects { get; init; }
    }
}