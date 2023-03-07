using System.Collections;
using DiskCardGame;
using UnityEngine;
using MyriadOfJSON.Parser;
using NCalc;

namespace MyriadOfJSON.Items.Actions;

public class ScaleBalance : ActionBase
{
    public string ExpressionStr { get; set; }
    public bool ToPlayer { get; set; }

    public ScaleBalance(string? expressionStr, bool? toPlayer)
    {
        ExpressionStr = expressionStr ?? "true";
        ToPlayer = toPlayer ?? false;
    }

    /* if false, evaluation failed */
    private bool EvaluateExpression(out int amount)
    {
        Expression? exp = ExpressionHandler.WorldPredicate(ExpressionStr);
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
