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

    private bool IsValidCustomItem()
        => ItemActions.ContainsKey(Data.name);

    private ActionList? GetItemActions()
        => IsValidCustomItem() ? ItemActions[Data.name] : null;

    private string? GetActivationCondition()
        => GetItemActions()?.Condition; 

    private IEnumerator ActivateCustomItem()
    {
        ActionList? x = GetItemActions();
        if (x == null) yield break;
        foreach (ActionBase action in x.Actions)
        {
            yield return action.Trigger();
        }
    }

    private bool CustomItemCondition()
    {
        string? condition = GetActivationCondition();
        if (condition == null) return true;
        /* activate if condition/item doesn't exist! nothing will happen. :T */

        Expression? exp = ExpressionHandler.WorldPredicate(condition); 
        return ExpressionHandler.SafeEvaluation(exp);
    }
}

