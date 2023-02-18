using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using InscryptionAPI.Items;
using DiskCardGame;
using GBC;
using UnityEngine;
using MiscellaneousJSON.Items.Actions;

namespace MiscellaneousJSON.Items;
public class DummyItem : ConsumableItem
{
    public static Dictionary<string, ActionBase> ItemActions = new();

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


}

