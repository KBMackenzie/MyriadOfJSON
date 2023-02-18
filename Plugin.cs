using BepInEx;
using HarmonyLib;

namespace MiscellaneousJSON;

// Plugin base:
[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
[BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
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
    }

    internal static void LogInfo(string message) => Instance?.Logger.LogInfo(message);
    internal static void LogError(string message) => Instance?.Logger.LogError(message);
}
