using System;
using System.Linq;
using System.Collections.Generic;
using MiscellaneousJSON.Helpers;
using DiskCardGame;

namespace MiscellaneousJSON.Parser;

public static class CardData
{
    public static Tribe GetTribe(string tribeName)
    {
        if (EnumHelpers.TryParse(tribeName, out Tribe t))
            return t; 
        
        (string, string) customTribeName = tribeName.GetGuidAndName();
        return ParserUtils.GetCustomTribe(customTribeName);
    }

    public static Ability GetAbility(string abilityName)
    {
        if (EnumHelpers.TryParse(abilityName, out Ability a))
            return a; 

        (string, string) customAbilityName = abilityName.GetGuidAndName();
        return ParserUtils.GetCustomAbility(customAbilityName);
    }

    public static SpecialTriggeredAbility GetSpecialAbility(string specialAbilityName)
    {
        if (EnumHelpers.TryParse(specialAbilityName, out SpecialTriggeredAbility s))
            return s;

        (string, string) customSpAbilityName = specialAbilityName.GetGuidAndName();
        return ParserUtils.GetCustomSpecialAbility(customSpAbilityName);
    }

}
