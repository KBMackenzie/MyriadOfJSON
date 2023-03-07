using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using UnityEngine;
using MyriadOfJSON.Items.Actions;
using NCalc;
using MyriadOfJSON.Parser;

namespace MyriadOfJSON.Items;

public class DummyItem : ConsumableItem
{
    /* All JSON items and their ActionList instances */
    public static Dictionary<string, ActionList> ItemActions = new();

    public override IEnumerator ActivateSequence()
    {
        base.PlayExitAnimation();
        /* custom item action! c: */
        yield return ActivateCustomItem();
        /* wait for dramatic effect. c: */
        yield return new WaitForSeconds(0.25f);       
        yield break;
    }

    public override bool ExtraActivationPrerequisitesMet()
    {
        if (!base.ExtraActivationPrerequisitesMet())
            return false;
        return CustomItemCondition();
    }

    private IEnumerator ActivateCustomItem()
    {
        string prefabId = Data.PrefabId;
        object? itemAction = ItemActions
            .Where(x => prefabId.EndsWith(x.Key))
            .FirstOrDefault();
        if (itemAction == null) yield break;

        ActionList x = ((KeyValuePair<string, ActionList>)itemAction).Value;
        foreach (ActionBase action in x.Actions)
        {
            yield return action.Trigger();
        }
    }

    private bool CustomItemCondition()
    {
        string prefabId = Data.PrefabId;
        object? itemAction = ItemActions
            .Where(x => prefabId.EndsWith(x.Key))
            .FirstOrDefault();

        /* activate if item doesn't exist! nothing will happen. :T */
        if (itemAction == null) return true;

        string? condition = ((KeyValuePair<string, ActionList>)itemAction).Value.Condition;
        if (condition == null) return true;

        Expression? exp = ExpressionHandler.WorldPredicate(condition); 
        return ExpressionHandler.SafeEvaluation(exp);
    }
}

