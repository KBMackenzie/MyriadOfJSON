using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using MiscellaneousJSON.Helpers;
using UnityEngine;
using NCalc;
using MiscellaneousJSON.Parser;

#pragma warning disable Publicizer001

namespace MiscellaneousJSON.Items.Actions;
public class GiveCurrency : ActionBase
{
    public string? Bones { get; set; }
    public string? Energy { get; set; }
    public string? Foils { get; set; }
    
    private int? ParseCurrency(string? x)
    {
        // TODO: Evaluate expression here instead! 
        /* if (!int.TryParse(x, out int n) || n < 0) return null; */

        Expression exp = new Expression(x);
        // Add parameters.
        // TODO
        return ExpHandler.SafelyParseAsInt(exp);
    } 

    public override IEnumerator Trigger()
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
    } }
