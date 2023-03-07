using System;
using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using MyriadOfJSON.Parser.Names;

namespace MyriadOfJSON.Parser.Functions;
using CardAction = System.Action<DiskCardGame.PlayableCard, string>;

public static class CardActions
{
    public static string[] Names =
    {
        FunctionNames.AddAbility,
        FunctionNames.AttackMod,
        FunctionNames.HealthMod
        // TODO
    };

    public static Dictionary<string, CardAction> Functions = new()
    {
        { FunctionNames.AddAbility.ToLower(), AddAbility },
        { FunctionNames.AttackMod.ToLower(), AttackMod },
        { FunctionNames.HealthMod.ToLower(), HealthMod } 
        // TODO
    };

    public static void AddAbility(PlayableCard card, string abilityParam)
        => card.AddTemporaryMod(new (CardData.GetAbility(abilityParam))); 
    
    public static void AttackMod(PlayableCard card, string attackParam)
    {
        if (!int.TryParse(attackParam, out int attackModifier))
        {
            Plugin.LogError($"Invalid attack param: {attackParam ?? "(null)"}");
            return;
        } 

        CardModificationInfo mod = new();
        mod.attackAdjustment += attackModifier;
        card.AddTemporaryMod(mod);
    }

    public static void HealthMod(PlayableCard card, string healthParam)
    {
        if (!int.TryParse(healthParam, out int healthModifier))
        {
            Plugin.LogError($"Invalid health param: {healthParam ?? "(null)"}");
            return;
        }

        CardModificationInfo mod = new();
        mod.healthAdjustment += healthModifier;
        card.AddTemporaryMod(mod);
    }
}
