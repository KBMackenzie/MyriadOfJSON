using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using MyriadOfJSON.Helpers;
using MyriadOfJSON.Parser;
using MyriadOfJSON.Parser.Functions;
using UnityEngine;
using InscryptionAPI.Card;
using Random = UnityEngine.Random;
using NCalc;

namespace MyriadOfJSON.Items.Actions;

public class DrawCardFromPool : ActionBase
{
    public string ExpressionStr { get; set; }
    public int CardAmount { get; set; } /* defaults to 1! */
    public string[] Callbacks { get; set; }

    public List<CardInfo>? CardPool { get; set; } // Filtered through with Predicate!

    public DrawCardFromPool(string? expressionStr, int? cardAmount, string[]? callbacks)
    {
        ExpressionStr = expressionStr ?? "true";
        CardAmount = cardAmount ?? 1;
        Callbacks = callbacks ?? new string[0];
    }

    public bool Predicate(CardInfo card)
    {
        /* Giant cards are silly! */
        if (card.HasTrait(Trait.Giant) || card.HasSpecialAbility(SpecialTriggeredAbility.GiantCard))
            return false;

        Expression? exp = ExpressionHandler.CardPredicate(ExpressionStr, card);
        return ExpressionHandler.SafeEvaluation(exp);
    }

    public List<CardInfo> CreateCardPool()
    {
        return CardManager.AllCardsCopy.Where(Predicate).ToList(); 
    }
    
    public override IEnumerator Trigger()
    {
        CardPool = CreateCardPool();
        for (int i = 0; i < CardAmount; i++)
        {
            int randomIndex = Random.Range(0, CardPool.Count - 1);
            CardInfo card = CardPool[randomIndex];

            Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, false);
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(
                    info: card,
                    temporaryMods: null, 
                    cardSpawnedCallback: (x) => AsCardAction.ParseAllFunctions(x, Callbacks)
                );
            yield return new WaitForSeconds(0.45f); 
        }
        yield break;
    }
}
