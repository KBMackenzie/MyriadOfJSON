using System.Collections.Generic;
using MiscellaneousJSON.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;

namespace MiscellaneousJSON.Parser;

// Type alias
using CardFunc = System.Func<DiskCardGame.CardInfo, string, MiscellaneousJSON.Parser.NCalcBool>;

public static class FunctionLib
{
    public static Dictionary<string, CardFunc> Functions = new();
    
    public static readonly string[] AllNames =
    {
        FunctionNames.HasTribe,
        FunctionNames.HasTrait,
        FunctionNames.HasAbility,
        FunctionNames.HasSpecialAbility,
        FunctionNames.HasMetaCategory
    };

    public static void PopulateFunctionDictionary()
    {
        Functions[FunctionNames.HasTribe] = HasTribe;
        Functions[FunctionNames.HasTrait] = HasTrait;
        Functions[FunctionNames.HasAbility] = HasAbility;
        Functions[FunctionNames.HasSpecialAbility] = HasSpecialAbility;
        Functions[FunctionNames.HasMetaCategory] = HasMetaCategory;
    }

    public static NCalcBool HasTribe(CardInfo card, string tribeName)
    {
        if (EnumHelpers.TryParse(tribeName, out Tribe t))
            return card.IsOfTribe(t);
        
        (string, string) customTribeName = tribeName.GetGuidAndName();
        Tribe customTribe = ParserUtils.GetCustomTribe(customTribeName);

        return card.IsOfTribe(customTribe);
    }

    public static NCalcBool HasTrait(CardInfo card, string traitName)
        => EnumHelpers.TryParse(traitName, out Trait t)
           && card.HasTrait(t);

    public static NCalcBool HasMetaCategory(CardInfo card, string metaCategoryName)
        => EnumHelpers.TryParse(metaCategoryName, out CardMetaCategory c)
           && card.HasCardMetaCategory(c);

    public static NCalcBool HasAbility(CardInfo card, string abilityName)
    {
        if (EnumHelpers.TryParse(abilityName, out Ability a))
            return card.HasAbility(a);

        (string, string) customAbilityName = abilityName.GetGuidAndName();
        Ability customAbility = ParserUtils.GetCustomAbility(customAbilityName);

        return card.HasAbility(customAbility);
    }

    public static NCalcBool HasSpecialAbility(CardInfo card, string specialAbilityName)
    {
        if (EnumHelpers.TryParse(specialAbilityName, out SpecialTriggeredAbility s))
            return card.HasSpecialAbility(s);

        (string, string) customSpAbilityName = specialAbilityName.GetGuidAndName();
        SpecialTriggeredAbility customSpAbility = ParserUtils.GetCustomSpecialAbility(customSpAbilityName);

        return card.HasSpecialAbility(customSpAbility);
    }
}
