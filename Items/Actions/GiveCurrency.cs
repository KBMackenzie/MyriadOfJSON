using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using MyriadOfJSON.Helpers;
using MyriadOfJSON.Parser;
using UnityEngine;
using NCalc;

#pragma warning disable Publicizer001

namespace MyriadOfJSON.Items.Actions;

// TODO!!!!!!!!!!!! IMPORTANT
// - Parse expression
public class GiveResources : ActionBase
{
    public enum Resource
    {
        None,
        Bones,
        Energy,
        Foils
    } 

    public Resource ResourceType; 
    public string? Expression;

    public GiveResources(string? resourceType, string? expression)
    {
        ResourceType = EnumHelpers.TryParse(resourceType?.SentenceCase(), out Resource c) ? c : Resource.None;
        Expression = expression;
    }

    private int? ParseCurrency(string? x)
    {
        // TODO: Evaluate expression here instead! 
        Expression exp = new Expression(x);
        // Add parameters.
        // TODO
        return ExpressionHandler.SafelyParseAsInt(exp);
    } 

    private IEnumerator GiveBones(int? amount)
    {
        if (amount is null) yield break;
        yield return Singleton<ResourcesManager>.Instance.AddBones(Math.Abs(amount.Value), null);
    }

    private IEnumerator GiveEnergy(int? amount)
    {
        if (amount is null) yield break;
        int energyAmount = Math.Abs(amount.Value);
        yield return Singleton<ResourcesManager>.Instance.AddMaxEnergy(energyAmount);
        yield return Singleton<ResourcesManager>.Instance.AddEnergy(energyAmount);
    }

    private IEnumerator GiveFoils(int? amount)
    {
        if (amount == null) yield break;
        int foilAmount = Math.Abs(amount.Value);
        yield return Singleton<CurrencyBowl>.Instance.ShowGain(foilAmount, false, true);
        RunState.Run.currency += foilAmount;
        yield return new WaitForSeconds(0.2f);
    }

    public override IEnumerator Trigger()
    {
        switch (ResourceType)
        {
            case Resource.Bones:
                yield return GiveBones(0 /*TODO: CHANGE THIS*/);
                yield break;
            case Resource.Energy:
                yield return GiveEnergy(0 /* TODO */);
                yield break;
            case Resource.Foils:
                yield return GiveFoils(0 /* TODO */);
                yield break;
            default:
                yield break;
        }
    }
}
