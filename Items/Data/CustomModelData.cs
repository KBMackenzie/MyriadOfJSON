using MyriadOfJSON.Helpers;
using InscryptionAPI.Items;
using UnityEngine;

namespace MyriadOfJSON.Items.Data;
using ModelType = ConsumableItemManager.ModelType;

public class CustomModelData
{
    public string? assetBundle { get; set; }
    public string? gameObjectName { get; set; }

    private GameObject? ModelCache { get; set; }
    
    public GameObject? GetModel(string guid, string name)
    {
        if (assetBundle == null || gameObjectName == null)
            return null;

        if (ModelCache != null) return ModelCache;

        if (BundleHelpers.TryLoadAssetBundle(assetBundle, out AssetBundle bundle)
                && bundle.TryLoadAsset(gameObjectName, out GameObject obj))
        {
            ModelCache = obj;
            return obj;
        }

        return null;
    }
}
