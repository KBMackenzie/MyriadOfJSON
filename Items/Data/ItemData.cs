using System;
using MyriadOfJSON.Helpers;
using InscryptionAPI.Items;
using DiskCardGame;
using InscryptionAPI.Items.Extensions;
using UnityEngine;

namespace MyriadOfJSON.Items.Data;
using ModelType = ConsumableItemManager.ModelType;

public class ItemJSONData
{
    public string? prefix { get; set; }
    
    public string? name { get; set; }
    public string? description { get; set; }
    public string? rulebookTexture { get; set; }

    public int? powerLevel { get; set; }
    public bool? notRandomlyGiven { get; set; }

    public string? modelType { get; set; }
    public bool? hasCustomModel { get; set; }
    public CustomModelData? customModelData { get; set; }

    /* fun ncalc! c: */
    public string? activationCondition { get; set; }
    public ActionListData? actions { get; set; }

    internal string GuidAndPrefix()
        => $"{Plugin.PluginGuid}_{prefix ?? string.Empty}";

    internal string GetName()
        => name ?? string.Empty;

    /* the internal name for an item, stored in ItemData.name */
    internal string InternalName()
        => $"{GuidAndPrefix()}_{GetName()}";

    internal ModelType ParseAsModel()
        => Enum.TryParse(modelType?.SentenceCase(), out ModelType type)
        ? type
        : ModelType.BasicRune; 

    internal GameObject? GetCustomModel()
        => customModelData?.GetModel(GuidAndPrefix(), GetName()); 

    internal bool HasCustomModel()
        => (hasCustomModel ?? false)
        && customModelData != null
        && GetCustomModel() != null;

    internal ConsumableItemData CreateItem()
        => !HasCustomModel()
        ? CreateItemWithABasicModel()
        : CreateItemWithACustomModel();

    private ConsumableItemData CreateItemWithABasicModel()
        => ConsumableItemManager.New(
                GuidAndPrefix(),
                GetName(),
                description ?? string.Empty,
                AssetHelpers.MakeTexture(rulebookTexture),
                typeof(DummyItem),
                ParseAsModel() 
                ).SetPowerLevel(powerLevel ?? 0)
        .SetNotRandomlyGiven(notRandomlyGiven ?? false)
        .SetAct1();

    private ConsumableItemData CreateItemWithACustomModel()
        => ConsumableItemManager.New(
                GuidAndPrefix(),
                GetName(),
                description ?? string.Empty,
                AssetHelpers.MakeTexture(rulebookTexture),
                typeof(DummyItem),
                GetCustomModel() 
            ).SetPowerLevel(powerLevel ?? 0)
            .SetNotRandomlyGiven(notRandomlyGiven ?? false)
            .SetAct1();

    internal void RegisterActions()
    {
        ActionList? allActions = actions?.CreateActions(InternalName(), activationCondition);
        if (allActions == null) return;
        DummyItem.ItemActions.Add(InternalName(), allActions);
    }
}

