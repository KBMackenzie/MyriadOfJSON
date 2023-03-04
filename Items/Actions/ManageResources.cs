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

public class ManageResources : ActionBase
{
    public enum Resource
    {
        None,
        Bones,
        Energy,
        Foils
    } 

    public Resource ResourceType { get; set; } 
    public string ExpressionStr { get; set; }

    public ManageResources(string? resourceType, string? expressionStr)
    {
        ResourceType = EnumHelpers.TryParse(resourceType?.SentenceCase(), out Resource c) ? c : Resource.None;
        ExpressionStr = expressionStr ?? "true";
    }

    private bool ParseAmount(out int amount)
    {
        Expression? exp = ExpressionHandler.WorldPredicate(ExpressionStr); 
        int? result = ExpressionHandler.SafelyParseAsInt(exp);
        amount = result ?? 0;
        return result != null;
    } 

    private IEnumerator ManageBones(int amount)
    {
        if (amount > 0)
            yield return Singleton<ResourcesManager>.Instance?.AddBones(amount);
        else
            yield return Singleton<ResourcesManager>.Instance?.SpendBones(Mathf.Abs(amount)); 
    }

    private IEnumerator ManageEnergy(int amount)
    {
        if (amount > 0)
            yield return Singleton<ResourcesManager>.Instance?.AddEnergy(amount);
        else
            yield return Singleton<ResourcesManager>.Instance?.SpendEnergy(Mathf.Abs(amount));
    }

    private IEnumerator ManageMaxEnergy(int amount)
    {
        yield return Singleton<ResourcesManager>.Instance.AddMaxEnergy(Mathf.Abs(amount));
    }

    private IEnumerator ManageCurrency(int amount)
    {
        RunState.Run.currency += amount;
        if (amount > 0)
            yield return Singleton<CurrencyBowl>.Instance?.DropWeightsIn(amount);
        else
            yield return Singleton<CurrencyBowl>.Instance?.TakeWeights(Mathf.Abs(amount));
        yield return new WaitForSeconds(0.2f);
    }

    public override IEnumerator Trigger()
    {
        if(!ParseAmount(out int amount)) yield break;
        
        switch (ResourceType)
        {
            case Resource.Bones:
                yield return ManageBones(amount);
                yield break;
            case Resource.Energy:
                yield return ManageEnergy(amount);
                yield break;
            case Resource.Foils:
                yield return ManageCurrency(amount);
                yield break;
            default:
                yield break;
        }
    }
}
