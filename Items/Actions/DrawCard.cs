using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using MyriadOfJSON.Helpers;
using MyriadOfJSON.Parser.Functions;
using UnityEngine;

namespace MyriadOfJSON.Items.Actions;

public class DrawCard : ActionBase
{
   string[] CardsToDraw { get; set; }
   /* callback functions! <3 (have to be parsed) */
   string[] Callbacks { get; set; }

   public DrawCard(string[]? cards, string[]? callbacks)
   {
       CardsToDraw = cards ?? new string[0];
       Callbacks = callbacks ?? new string[0];
   }

   public override IEnumerator Trigger()
   {
        foreach (string? cardName in CardsToDraw)
        {
            CardInfo? card = CardHelpers.Get(cardName); 
            if (card == null) yield break;

            Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, false);
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(
                    info: card,
                    temporaryMods: null,
                    cardSpawnedCallback: (x) => AsCardAction.ParseAllFunctions(x, Callbacks)
                );
            yield return new WaitForSeconds(0.45f); // Maybe too long? TODO
        }
        yield break;
   } 
}
