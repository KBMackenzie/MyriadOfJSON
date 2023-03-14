using System.IO;
using Newtonsoft.Json;
using MyriadOfJSON.Helpers;
using MyriadOfJSON.Items.Data;
using BepInEx;

namespace MyriadOfJSON.Items;

public static class LoadItems
{
    private static string[] FindItems()
        => Directory.GetFiles(Paths.PluginPath, "*_item.json", SearchOption.AllDirectories);

    internal static void LoadAll()
        => FindItems().ForEach(LoadJSON);
    
    internal static void LoadJSON(string filePath)
    {
        ItemJSONData? data;
        try
        {
             data = JsonConvert.DeserializeObject<ItemJSONData>(File.ReadAllText(filePath));
        }
        catch (JsonException ex)
        {
            Plugin.LogError($"Couldn't load JSON data from file \'{Path.GetFileName(filePath)}\'!");
            Plugin.LogError(ex.Message);
            return;
        }
        if (data == null) return;
        data.CreateItem();
        data.RegisterActions();
        Plugin.LogInfo($"Loaded new item from file '{Path.GetFileName(filePath)}'!");
 
    }
}
