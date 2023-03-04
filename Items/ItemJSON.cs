using System;
using System.Linq;
using System.Collections.Generic;
using InscryptionAPI.Items;
using DiskCardGame;
using UnityEngine;
using InscryptionAPI.Items.Extensions;
using MyriadOfJSON.Helpers;

namespace MyriadOfJSON.Items;
using ModelType = ConsumableItemManager.ModelType;

public class ItemData
{
    public string? prefix { get; set; }
    
    public string? name { get; set; }
    public string? description { get; set; }
    public string? rulebookTexture { get; set; }

    public int? powerLevel { get; set; }
    public bool? notRandomlyGiven { get; set; }

    public string? modelType { get; set; }
    public bool hasCustomModel { get; set; }
    public CustomModelData? customModelData { get; set; }

    private ModelType GetModelType()
    {
        if (customModelData != null)
            return customModelData.MakeModel();

        return EnumHelpers.TryParse(modelType?.SentenceCase(), out ModelType mt)
                ? mt
                : ModelType.BasicRune; 
    }

    internal ConsumableItemData CreateItem()
        => ConsumableItemManager.New(
                prefix,
                name ?? string.Empty,
                description ?? string.Empty,
                AssetHelpers.MakeTexture(rulebookTexture),
                typeof(DummyItem),
                GetModelType() 
            ).SetPowerLevel(powerLevel ?? 0)
            .SetNotRandomlyGiven(notRandomlyGiven ?? false)
            .SetAct1();
}

public class CustomModelData
{
    public string? assetBundle { get; set; }
    public string? gameObjectName { get; set; }

    private static ModelType DefaultModelType
        => ModelType.BasicRune;
    
    public ModelType MakeModel()
    {
        if (assetBundle == null || gameObjectName == null)
            return DefaultModelType;

        AssetBundle ab = AssetBundle.LoadFromFile(assetBundle);
        return DefaultModelType;
    }
}
