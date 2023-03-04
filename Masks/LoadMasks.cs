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
        MaskData? mask = JsonConvert.DeserializeObject<MaskData>(File.ReadAllText(file));

        if (mask == null)
        {
            Plugin.LogError(""); // TODO
            return;
        }

        mask.overrideMask = mask.overrideMask?.SentenceCase(); // Format override name

        if (!ValidOverride(mask)) 
        {
            Plugin.LogError($"Invalid mask override name: {mask.overrideMask ?? "(null)"}");
            return;
        }

        mask.MakeMask();
    }
}
