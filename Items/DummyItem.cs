using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using InscryptionAPI.Items;
using DiskCardGame;
using GBC;
using UnityEngine;
using MyriadOfJSON.Items.Actions;
using HarmonyLib;
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
        // Wait for dramatic effect.
        yield return new WaitForSeconds(0.25f);       
        yield break;
    }

    public override bool ExtraActivationPrerequisitesMet()
    {
        if (!base.ExtraActivationPrerequisitesMet())
            return false;
        return true;
    }

    /* Run actions for item! */
    [HarmonyPatch(typeof(DummyItem), nameof(DummyItem.ActivateSequence))]
    [HarmonyPostfix]
    private static IEnumerator ActivatePatch(IEnumerator enumerator, DummyItem __instance)
    {
        yield return enumerator;

        /* The prefab ID for custom items in the CustomItemManager.New() method thingy is set as :
            string prefabID = (data.name = pluginGUID + "_" + data.rulebookName);
        I can use that! c: */

        string prefabId = __instance.Data.PrefabId;

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

    /* Check item's extra condition for activation! */
    [HarmonyPatch(typeof(DummyItem), nameof(DummyItem.ExtraActivationPrerequisitesMet))]
    [HarmonyPostfix]
    private static void PrerequisitesPatch(ref DummyItem __instance, ref bool __result)
    {
        // If __result is already false, then no need to do anything.
        if (!__result) return;

        string prefabId = __instance.Data.PrefabId;

        var itemAction = ItemActions
            .Where(x => prefabId.EndsWith(x.Key))
            .FirstOrDefault();

        if (itemAction.Value == null) return;

        string? condition = itemAction.Value.Condition;
        if (condition == null) return;

        Expression exp = new(condition);
        // Add parameters to exp here.
        // TODO
        bool newResult = ExpressionHandler.SafeEvaluation(exp);

        __result = newResult;
    }
}

