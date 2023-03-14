using System;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using BepInEx;
using MyriadOfJSON.Helpers;
using DiskCardGame;

namespace MyriadOfJSON.Masks;

internal static class LoadMasks
{
    private static readonly string[] MaskNames
        = Enum.GetNames(typeof(LeshyAnimationController.Mask));

    private static string[] GetFiles()
        => Directory.GetFiles(Paths.PluginPath, "*_mask.json", SearchOption.AllDirectories);

    public static void LoadAll()
        => GetFiles().ForEach(LoadJSON);

    private static bool ValidOverride(MaskData mask)
        => mask.overrideMask != null && MaskNames.Contains(mask.overrideMask);

    internal static void LoadJSON(string file)
    {

        MaskData? data; 
        try
        {
            data = JsonConvert.DeserializeObject<MaskData>(File.ReadAllText(file));
        }
        catch (JsonException ex)
        {
            Plugin.LogError($"Couldn't load JSON data from \'{Path.GetFileName(file)}\'!"); 
            Plugin.LogError(ex.Message);
            return;
        }
        if (data == null) return;
        data.overrideMask = data.overrideMask?.SentenceCase(); // Format override name
        if (!ValidOverride(data)) 
        {
            Plugin.LogError($"Invalid mask override name: {data.overrideMask ?? "(null)"}");
            return;
        }

        data.MakeMask();
    }
}
