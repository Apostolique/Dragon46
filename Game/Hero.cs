namespace GameProject
{
    public class HeroType : BaseCharacterType
    {
        public HeroType(CharacterType type, int maxHP, string name, int baseArmour, int baseMagicResistance, int baseStrength, int baseIntelligence, string abilitySound)
            : base(type, maxHP, name, baseArmour, baseMagicResistance, baseStrength, baseIntelligence, abilitySound)
        {

        }
    }
}