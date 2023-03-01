using BepInEx;
using HarmonyLib;
using MiscellaneousJSON.Pelts;
using MiscellaneousJSON.Masks;
using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using DiskCardGame;

namespace MiscellaneousJSON;

// Plugin base:
[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
[BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency("MADH.inscryption.JSONLoader", BepInDependency.DependencyFlags.SoftDependency)]
public class Plugin : BaseUnityPlugin
{   
    public const string PluginGuid = "kel.inscryption.miscjson";
    public const string PluginName = "MiscellaneousJSON";
    public const string PluginVersion = "1.0.0";
    
    internal static Plugin? Instance; // Log source.
    
    private void Awake()
    {
        Instance = this; // Make log source.

        Harmony harmony = new Harmony("kel.harmony.miscjson");
        harmony.PatchAll();

        Dummy dummy = new(); 
        System.Console.WriteLine($"Ability: {dummy.Ability}");
        // LoadPelts.LoadAll();
        // LoadMasks.LoadAll();
    }

    internal static void LogInfo(string message) => Instance?.Logger.LogInfo(message);
    internal static void LogError(string message) => Instance?.Logger.LogError(message);
}

public class Dummy : AbilityBehaviour
{
    private static Ability ability = AbilityManager.New(Plugin.PluginGuid, "dummy", "", typeof(Dummy), "").ability;
    public override Ability Ability => ability; 
}
