using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using MiscellaneousJSON.Helpers;
using UnityEngine;

namespace MiscellaneousJSON.Items.Actions;
public class DrawCard : ActionBase
{
   List<string>? CardsToDraw { get; set; }

   public override IEnumerator TriggerItem()
   {
        if (CardsToDraw == null) yield break;
        foreach (string? cardName in CardsToDraw)
        {
            CardInfo? card = CardHelpers.Get(cardName); 
            if (card == null) yield break;

            Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, false);
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(
                    info: card,
                    temporaryMods: new List<CardModificationInfo>(),
                    spawnOffset: new Vector3(0f, 0f, 0f),
                    onDrawnTriggerDelay: 0f
                );
            yield return new WaitForSeconds(0.45f); 
        }
        yield break;
   } 
}
