using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using MiscellaneousJSON.Helpers;
using UnityEngine;

namespace MiscellaneousJSON.Items.Actions;
public class GiveCurrency : ActionBase
{
    public string? Bones { get; set; }
    public string? Energy { get; set; }
    public string? Foils { get; set; }
    
    private int? ParseCurrency(string? x)
    {
        if (!int.TryParse(x, out int n)
            || n < 0) return null;
        return n;
    } 

    public override IEnumerator TriggerItem()
    {
        int? addBones = ParseCurrency(Bones);
        if (addBones != null)
        {
            yield return Singleton<ResourcesManager>.Instance.AddBones(addBones.Value); 
        }

        int? addEnergy = ParseCurrency(Energy);
        if (addEnergy != null)
        {
            yield return Singleton<ResourcesManager>.Instance.AddEnergy(addEnergy.Value);  
        }
        
        int? addFoils = ParseCurrency(Energy);
        if (addFoils != null)
        {
            yield return Singleton<CurrencyBowl>.Instance.DropWeightsIn(addFoils.Value);
        }

        yield break;
    }
}
