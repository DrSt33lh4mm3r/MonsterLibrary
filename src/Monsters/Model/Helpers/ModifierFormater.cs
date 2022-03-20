namespace MonsterLibrary.Monsters.Model.Helpers
{
    public class ModifierFormater
    {
        public static string FormatModifier(int modifier, bool wrapInParantheses = false)
        { 
            string mod; 

            if (modifier < 0)
            {
                mod = "" + modifier;
            } 
            else 
            {
                mod = "+" + modifier;
            }

            if (wrapInParantheses) 
            {
                mod = "(" + mod + ")";
            }

            return " " + mod;
        }
    }
}