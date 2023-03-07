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
        ItemData? data = JsonConvert.DeserializeObject<ItemData>(File.ReadAllText(filePath));
        if(data == null)
        {
            Plugin.LogError($"Couldn't load JSON data from file \'{Path.GetFileName(filePath)}\'!");
            return;
        }
        data.CreateItem();
        data.RegisterActions();
    }
}
