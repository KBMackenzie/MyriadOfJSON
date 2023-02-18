using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using InscryptionAPI.Items;
using DiskCardGame;
using GBC;
using UnityEngine;
using MiscellaneousJSON.Items.Actions;
using HarmonyLib;
using NCalc;
using MiscellaneousJSON.Items.Parser;

namespace MiscellaneousJSON.Items;
public class DummyItem : ConsumableItem
{
    public static Dictionary<string, ActionList> ItemActions = new();

    // Patch that runs something specific for this IEnumerator for a custom item.
    public override IEnumerator ActivateSequence()
    {
        base.PlayExitAnimation();
        yield return new WaitForSeconds(0.25f);       
        yield break;
    }

    // Patch that changes the value *if* it returned true.
    public override bool ExtraActivationPrerequisitesMet()
    {
        if (!base.ExtraActivationPrerequisitesMet())
        {
            return false;
        }
        return true;
    }

    [HarmonyPatch(typeof(DummyItem), nameof(DummyItem.ActivateSequence))]
    [HarmonyPostfix]
    private static IEnumerator ActivatePatch(IEnumerator enumerator, DummyItem __instance)
    {
        yield return enumerator;

        /* The prefab ID for custom items in the CustomItemManager.New() method thingy is set as :
            string prefabID = (data.name = pluginGUID + "_" + data.rulebookName);
        I can use that! c: */

        string prefabId = __instance.Data.PrefabId;

        var itemAction = ItemActions
            .Where(x => prefabId.EndsWith(x.Key))
            .FirstOrDefault();

        if (itemAction.Value == null) yield break;
        ActionList x = itemAction.Value;

        foreach (ActionBase action in x.Actions)
        {
            yield return action.Trigger();
        }
    }

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

