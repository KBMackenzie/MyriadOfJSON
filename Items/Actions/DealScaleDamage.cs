using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using MiscellaneousJSON.Helpers;
using UnityEngine;

namespace MiscellaneousJSON.Items.Actions;
public class DealScaleDamage : ActionBase
{
    public string? DamageAmount { get; set; }

    public override IEnumerator TriggerItem()
    {
        int? damage = int.TryParse(DamageAmount, out int n) ? n : null;
        if (damage == null) yield break;
        
        if (damage > 0)
        {
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(
                damage: damage.Value,
                numWeights: damage.Value,
                toPlayer: false,
                changeView: true
            );
        }
        else if (damage < 0)
        {
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(
                damage: Mathf.Abs(damage.Value),
                numWeights: Mathf.Abs(damage.Value),
                toPlayer: true,
                changeView: true
            );
        }

        yield break;
    }
}
