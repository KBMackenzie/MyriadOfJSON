using InscryptionAPI.Items;
using UnityEngine;

namespace MyriadOfJSON.Items.Data;
using ModelType = ConsumableItemManager.ModelType;

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
        // TODO
        return DefaultModelType;
    }
}
