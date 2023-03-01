using System;
using System.Linq;
using System.Collections.Generic;
using InscryptionAPI.Guid;
using InscryptionAPI.Card;
using DiskCardGame;

namespace MiscellaneousJSON.Parser;

public static class ParserUtils
{
    public static Tribe GetCustomTribe(string guid, string rulebookName)
        => GuidManager.GetEnumValue<Tribe>(guid, rulebookName);

    public static Ability GetCustomAbility(string guid, string rulebookName)
        => GuidManager.GetEnumValue<Ability>(guid, rulebookName);

    public static SpecialTriggeredAbility GetCustomSpecialAbility(string guid, string rulebookName)
        => GuidManager.GetEnumValue<SpecialTriggeredAbility>(guid, rulebookName);

    private static string? GetAbilityName(string abilityName)
    {
        AbilityManager.FullAbility? x = AbilityManager.AllAbilities
            .Where(x => x.Info.name == abilityName)
            .FirstOrDefault();  
        return x?.Info?.name;
    }
}
