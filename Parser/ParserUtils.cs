using System;
using System.Linq;
using System.Collections.Generic;
using InscryptionAPI.Guid;
using InscryptionAPI.Card;
using DiskCardGame;

namespace MyriadOfJSON.Parser;

public static class ParserUtils
{
    public static Tribe GetCustomTribe((string guid, string name) tribe)
        => GuidManager.GetEnumValue<Tribe>(tribe.guid, tribe.name);

    public static Ability GetCustomAbility((string guid, string name) ability) 
        => GuidManager.GetEnumValue<Ability>(ability.guid, ability.name);

    public static SpecialTriggeredAbility GetCustomSpecialAbility((string guid, string name) specialAbility)
        => GuidManager.GetEnumValue<SpecialTriggeredAbility>(specialAbility.guid, specialAbility.name);
}
