using System.Collections;
using DiskCardGame;
using UnityEngine;

namespace MiscellaneousJSON.Items.Actions;

public class DealScaleDamage : ActionBase
{
    public int? DamageAmount { get; set; }

    public override IEnumerator Trigger()
    {
        int? damage = DamageAmount;

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
