using System;
using MyriadOfJSON.Helpers;
using DiskCardGame;

namespace MyriadOfJSON.Parser;

public static class CardData
{
    public static Tribe GetTribe(string tribeName)
    {
        if (Enum.TryParse(tribeName, out Tribe t))
            return t; 
        
        (string, string) customTribeName = tribeName.GetGuidAndName();
        return ParserUtils.GetCustomTribe(customTribeName);
    }

    public static Ability GetAbility(string abilityName)
    {
        if (Enum.TryParse(abilityName, out Ability a))
            return a; 

        (string, string) customAbilityName = abilityName.GetGuidAndName();
        return ParserUtils.GetCustomAbility(customAbilityName);
    }

    public static SpecialTriggeredAbility GetSpecialAbility(string specialAbilityName)
    {
        if (Enum.TryParse(specialAbilityName, out SpecialTriggeredAbility s))
            return s;

        (string, string) customSpAbilityName = specialAbilityName.GetGuidAndName();
        return ParserUtils.GetCustomSpecialAbility(customSpAbilityName);
    }

}
