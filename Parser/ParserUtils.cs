using System;
using System.Linq;
using System.Collections.Generic;
using InscryptionAPI.Guid;
using InscryptionAPI.Card;
using DiskCardGame;

namespace MiscellaneousJSON.Parser;

public static class ParserUtils
{
    private static Ability GetAbility(string guid, string rulebookName)
        => GuidManager.GetEnumValue<Ability>(guid, rulebookName);

    private static string? GetAbilityName(string abilityName)
    {
        AbilityManager.FullAbility? x = AbilityManager.AllAbilities
            .Where(x => x.Info.name == abilityName)
            .FirstOrDefault();  
        return x?.Info?.name;
    }

    /*
     * public static Ability GetCustomAbility(string GUID, string rulebookname)
     * {
     *     return InscryptionAPI.Guid.GuidManager.GetEnumValue<Ability>(GUID, rulebookname);
     *
     *     }
     */
}
