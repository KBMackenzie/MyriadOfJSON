using System.Collections.Generic;
using MiscellaneousJSON.Helpers;
using DiskCardGame;

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
        Functions[FunctionNames.HasTrait] =
            (card, exp) =>
                EnumHelpers.TryParse<Trait>(exp, out Trait x)
                && card.HasTrait(x);

        // Functions[FunctionNames.HasTribe] =
    }
}
