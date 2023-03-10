using System;
using System.Collections.Generic;
using MyriadOfJSON.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using MyriadOfJSON.Parser.Names;

namespace MyriadOfJSON.Parser.Functions;

// Type alias
using CardFunc = System.Func<DiskCardGame.CardInfo, string, MyriadOfJSON.Parser.NCalcBool>;

public static class CardPredicates
{
    public static readonly string[] Names =
    {
        FunctionNames.HasTribe,
        FunctionNames.HasTrait,
        FunctionNames.HasAbility,
        FunctionNames.HasSpecialAbility,
        FunctionNames.HasMetaCategory,
        FunctionNames.HasAppearanceBehaviour,
        FunctionNames.HasMoxCost
    };

    public static Dictionary<string, CardFunc> Functions = new()
    {
        { FunctionNames.HasTribe.ToLower(), HasTribe },
        { FunctionNames.HasTrait.ToLower(), HasTrait },
        { FunctionNames.HasAbility.ToLower(), HasAbility },
        { FunctionNames.HasSpecialAbility.ToLower(), HasSpecialAbility },
        { FunctionNames.HasMetaCategory.ToLower(), HasMetaCategory },
        { FunctionNames.HasAppearanceBehaviour.ToLower(), HasAppearance },
        { FunctionNames.HasMoxCost.ToLower(), HasMoxCost }
    };

    public static NCalcBool HasTribe(CardInfo card, string tribeName)
        => card.IsOfTribe(CardData.GetTribe(tribeName));

    public static NCalcBool HasTrait(CardInfo card, string traitName)
        => Enum.TryParse(traitName, out Trait t)
           && card.HasTrait(t);

    public static NCalcBool HasMetaCategory(CardInfo card, string metaCategoryName)
        => Enum.TryParse(metaCategoryName, out CardMetaCategory c)
           && card.HasCardMetaCategory(c);

    public static NCalcBool HasAppearance(CardInfo card, string appearanceName)
        => Enum.TryParse(appearanceName, out CardAppearanceBehaviour.Appearance a)
           && card.appearanceBehaviour.Contains(a);

    public static NCalcBool HasAbility(CardInfo card, string abilityName)
        => card.HasAbility(CardData.GetAbility(abilityName));

    public static NCalcBool HasSpecialAbility(CardInfo card, string specialAbilityName)
        => card.HasSpecialAbility(CardData.GetSpecialAbility(specialAbilityName));

    public static NCalcBool HasMoxCost(CardInfo card, string moxCostName)
        => Enum.TryParse(moxCostName, out GemType gemCost)
            && card.GemsCost.Contains(gemCost);
}
