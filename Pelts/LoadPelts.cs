using BepInEx;
using System.IO;
using Newtonsoft.Json;
using MyriadOfJSON.Helpers;

namespace MyriadOfJSON.Pelts;

internal static class LoadPelts
{
    private static string[] FindPelts()
        => Directory.GetFiles(Paths.PluginPath, "*_pelt.json", SearchOption.AllDirectories);

    internal static void LoadAll()
        => FindPelts().ForEach(LoadJSON);
    
    internal static void LoadJSON(string filePath)
    {
        PeltData? data = JsonConvert.DeserializeObject<PeltData>(File.ReadAllText(filePath));
        if(data == null )
        {
            Plugin.LogError($"Couldn't load JSON data from file \'{Path.GetFileName(filePath)}\'!");
            return;
        }
        data.MakePelt();
    }
}
