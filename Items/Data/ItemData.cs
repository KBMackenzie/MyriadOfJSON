using InscryptionAPI.Items;
using DiskCardGame;
using InscryptionAPI.Items.Extensions;
using MyriadOfJSON.Helpers;

namespace MyriadOfJSON.Items.Data;
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

    /* fun ncalc! c: */
    public string? activationCondition { get; set; }
    public ActionListData? actions { get; set; }

    internal string GetItemName()
        => $"{prefix}_{name}";

    internal ModelType GetModelType()
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

    internal void RegisterActions()
    {
        ActionList? allActions = actions?.CreateActions(GetItemName(), activationCondition);
        if (allActions == null) return;
        DummyItem.ItemActions.Add(GetItemName(), allActions);
    }
}

