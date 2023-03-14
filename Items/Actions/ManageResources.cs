using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using MyriadOfJSON.Helpers;
using MyriadOfJSON.Parser;
using MyriadOfJSON.Items.Data;
using MyriadOfJSON.Parser.Variables;
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

    public Resource ResourceType { get; } 
    public string AmountExpression { get; }

    public ManageResources(ManageResourcesData data)
    {
        ResourceType = Enum.TryParse(
                    data.resourceType?.SentenceCase(),
                    out Resource c
                ) ? c : Resource.None;
        AmountExpression = data.expression ?? "true";
        SetOrder(data);
    }

    private bool ParseAmount(out int amount)
    {
        Expression? exp = ExpressionHandler.WorldPredicate(AmountExpression); 
        int? result = ExpressionHandler.SafelyParseAsInt(exp);
        amount = result ?? 0;
        return result != null;
    } 

    private IEnumerator ManageBones(int amount)
    {
        Plugin.LogInfo($"Bone amount: {VariableUtils.BoneAmount()}");
        if (amount > 0)
            yield return Singleton<ResourcesManager>.Instance?.AddBones(amount);
        else if (amount < 0)
        {
            int takeAmount = Mathf.Min(Mathf.Abs(amount), VariableUtils.BoneAmount()); 
            Plugin.LogInfo($"Take amount: {takeAmount}");
            yield return Singleton<ResourcesManager>.Instance?.SpendBones(takeAmount);
        }
    }

    private IEnumerator ManageEnergy(int amount)
    {
        if (amount > 0)
            yield return Singleton<ResourcesManager>.Instance?.AddEnergy(amount);
        else if (amount < 0)
        { 
            int takeAmount = Mathf.Min(Mathf.Abs(amount), VariableUtils.EnergyAmount()); 
            yield return Singleton<ResourcesManager>.Instance?.SpendEnergy(takeAmount);
        }
    }

    private IEnumerator ManageMaxEnergy(int amount)
    {
        yield return Singleton<ResourcesManager>.Instance?.AddMaxEnergy(Mathf.Abs(amount));
    }

    private IEnumerator ManageCurrency(int amount)
    {
        RunState.Run.currency += amount;
        if (amount > 0)
            yield return Singleton<CurrencyBowl>.Instance?.DropWeightsIn(amount);
        else if (amount < 0)
        {
            int takeAmount = Mathf.Min(Mathf.Abs(amount), VariableUtils.FoilAmount()); 
            yield return Singleton<CurrencyBowl>.Instance?.TakeWeights(takeAmount);
        }
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
