using System.Collections.Generic;
using MiscellaneousJSON.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using MiscellaneousJSON.Parser.Names;

namespace MiscellaneousJSON.Parser.Functions;

// Type alias
using CardFunc = System.Func<DiskCardGame.CardInfo, string, MiscellaneousJSON.Parser.NCalcBool>;

public static class CardPredicates
{
    public static readonly string[] Names =
    {
        FunctionNames.HasTribe,
        FunctionNames.HasTrait,
        FunctionNames.HasAbility,
        FunctionNames.HasSpecialAbility,
        FunctionNames.HasMetaCategory
    };

    public static Dictionary<string, CardFunc> Functions = new()
    {
        { FunctionNames.HasTribe, HasTribe },
        { FunctionNames.HasTrait, HasTrait },
        { FunctionNames.HasAbility, HasAbility },
        { FunctionNames.HasSpecialAbility, HasSpecialAbility },
        { FunctionNames.HasMetaCategory, HasMetaCategory },
        { FunctionNames.HasAppearanceBehaviour, HasAppearance }
    };

    public static NCalcBool HasTribe(CardInfo card, string tribeName)
        => card.IsOfTribe(CardData.GetTribe(tribeName));

    public static NCalcBool HasTrait(CardInfo card, string traitName)
        => EnumHelpers.TryParse(traitName, out Trait t)
           && card.HasTrait(t);

    public static NCalcBool HasMetaCategory(CardInfo card, string metaCategoryName)
        => EnumHelpers.TryParse(metaCategoryName, out CardMetaCategory c)
           && card.HasCardMetaCategory(c);

    public static NCalcBool HasAppearance(CardInfo card, string appearanceName)
        => EnumHelpers.TryParse(appearanceName, out CardAppearanceBehaviour.Appearance a)
           && card.appearanceBehaviour.Contains(a);

    public static NCalcBool HasAbility(CardInfo card, string abilityName)
        => card.HasAbility(CardData.GetAbility(abilityName));

    public static NCalcBool HasSpecialAbility(CardInfo card, string specialAbilityName)
        => card.HasSpecialAbility(CardData.GetSpecialAbility(specialAbilityName));
}
