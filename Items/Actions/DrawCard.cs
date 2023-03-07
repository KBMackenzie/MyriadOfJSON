using System.Collections;
using DiskCardGame;
using MyriadOfJSON.Helpers;
using MyriadOfJSON.Items.Data;
using MyriadOfJSON.Parser.Functions;
using UnityEngine;

namespace MyriadOfJSON.Items.Actions;

public class DrawCard : ActionBase
{
   string[] CardsToDraw { get; set; }
   /* callback functions! <3 (have to be parsed) */
   string[] Callbacks { get; set; }

   public DrawCard(DrawCardsData data)
   {
       CardsToDraw = data.cardNames ?? new string[0]; 
       Callbacks = data.callbacks ?? new string[0];
       SetOrder(data);
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
