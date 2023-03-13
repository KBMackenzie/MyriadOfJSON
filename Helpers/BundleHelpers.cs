using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using BepInEx;

namespace MyriadOfJSON.Helpers;

public static class BundleHelpers
{
    /* TODO:
     * AssetBundle helpers
     * GameObject Mesh helpers */

    #nullable disable
    public static bool TryLoadAssetBundle(string fileName, out AssetBundle bundle)
    {
        try
        {
            string fullPath = Directory.GetFiles(
                        path: Paths.PluginPath,
                        searchPattern: fileName,
                        searchOption: SearchOption.AllDirectories
                    ).First();
            bundle = AssetBundle.LoadFromFile(fullPath);
            return bundle != null;
        }
        catch (Exception ex)
        {
            Plugin.LogError($"Couldn't load asset bundle from file '{fileName ?? "(null)"}'!");
            Plugin.LogError(ex.Message);
            bundle = null;
            return false;
        }
    }
    
    public static bool TryLoadAsset<T>(this AssetBundle bundle,
            string objName,
            out T output) where T : UnityEngine.Object
    {
        try
        {
            output = bundle.LoadAsset<T>(objName);
            return output != default(T);
        }
        catch (Exception ex)
        {
            Plugin.LogError($"Couldn't load asset '{objName ?? "(null)"}' of type '{typeof(T)}' from bundle '{bundle.name}'!");
            Plugin.LogError(ex.Message);
            output = default(T);
            return false;
        }
    }

}
