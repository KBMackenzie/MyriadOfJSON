using System.Collections;
using MyriadOfJSON.Parser;
using MyriadOfJSON.Items.Data;
using DiskCardGame;
using UnityEngine;

using NCalc;

namespace MyriadOfJSON.Items.Actions;

public class ScaleBalance : ActionBase
{
    public string AmountExpression { get; }
    public bool ToPlayer { get; }

    public ScaleBalance(ScaleBalanceData data)
    {
        AmountExpression = data.expression ?? "true";
        ToPlayer = data.toPlayer ?? false;
        SetOrder(data);
    }

    /* if false, evaluation failed */
    private bool EvaluateExpression(out int amount)
    {
        Expression? exp = ExpressionHandler.WorldPredicate(AmountExpression);
        int? result = ExpressionHandler.SafelyParseAsInt(exp);
        amount = result ?? 0;
        return result != null;
    }

    public override IEnumerator Trigger()
    {
        if (!EvaluateExpression(out int amount)) yield break;
        amount = Mathf.Abs(amount);

        yield return Singleton<LifeManager>.Instance?.ShowDamageSequence(
                    damage: amount,
                    numWeights: amount,
                    toPlayer: ToPlayer 
                );
        yield break;
    }
}
