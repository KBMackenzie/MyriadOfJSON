using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using MyriadOfJSON.Helpers;
using UnityEngine;
using InscryptionAPI.Card;
using Random = UnityEngine.Random;

namespace MyriadOfJSON.Items.Actions;

// TODO!!!!!!!!!!!! IMPORTANT
// - Parse expression
// - Add callbacks
public class DrawCardFromPool : ActionBase
{
    public List<CardInfo>? CardPool { get; set; } // Filtered through with Predicate!
    public int? CardAmount { get; set; }
    public Func<CardInfo, bool>? Predicate { get; set; }

    public void CreateCardPool()
    {
        if (Predicate == null) return;
        CardPool = CardManager.AllCardsCopy.Where(Predicate).ToList(); 
    }
    
    public override IEnumerator Trigger()
    {
        if (CardPool == null) yield break;
        CardAmount ??= 1;
        for (int i = 0; i < CardAmount; i++)
        {
            int randomIndex = Random.Range(0, CardPool.Count - 1);
            CardInfo card = CardPool[randomIndex];

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
