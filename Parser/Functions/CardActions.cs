using System;
using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using MyriadOfJSON.Parser.Names;
using System.Text.RegularExpressions;

namespace MyriadOfJSON.Parser.Functions;
using CardAction = System.Action<DiskCardGame.PlayableCard, string>;

public static class CardActions
{
    public static string[] Names =
    {
        FunctionNames.AddAbility,
        FunctionNames.AttackMod,
        FunctionNames.HealthMod,
        // TODO
    };

    public static Dictionary<string, CardAction> Functions = new()
    {
        { FunctionNames.AddAbility.ToLower(), AddAbility },
        { FunctionNames.AttackMod.ToLower(), AttackMod },
        { FunctionNames.HealthMod.ToLower(), HealthMod },
        // TODO
    };

    public static void AddAbility(PlayableCard card, string abilityParam)
    {
        CardModificationInfo mod = new(CardData.GetAbility(abilityParam));
        mod.fromCardMerge = true;
        card.Info.Mods.Add(mod);
        // card.AddTemporaryMod(mod); 
        card.RenderCard();
    } 
    
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


    /* MOVE LOGIC */
    /* [<>]n
     * Where < is left and > is right, and 'n' is the amount of slots to move.
     * The card will move as much as it can until it hits a full slot.
     * <2 -- Move twice to the left. >2 -- Move twice to the right. */
    public class MoveInfo
    {
        public static Regex MoveRegex = new(@"^[<>]\d$");

        public bool IsLeft { get; }
        public int Amount { get; }

        public MoveInfo(bool isLeft, int amount)
        {
            IsLeft = isLeft;
            Amount = amount;
        }
    }

    private static MoveInfo? ParseDirection(string direction)
    {
        if (!MoveInfo.MoveRegex.IsMatch(direction))
        {
            Plugin.LogError($"Invalid move expression: {direction}");
            return null;
        }
        bool isLeft = direction[0] == '<';
        int amount = int.TryParse(direction[1].ToString(), out int x) ? x : 0;
        return new (isLeft, amount);
    }
}
