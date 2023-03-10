using BepInEx;
using HarmonyLib;
using DiskCardGame;
using BepInEx.Configuration;
using InscryptionAPI.Card;
using MyriadOfJSON.Pelts;
using MyriadOfJSON.Masks;
using MyriadOfJSON.Items;
using System;
using Newtonsoft.Json;
using MyriadOfJSON.Items.Data;

namespace MyriadOfJSON;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
[BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency("MADH.inscryption.JSONLoader", BepInDependency.DependencyFlags.SoftDependency)]
public class Plugin : BaseUnityPlugin
{   
    public const string PluginGuid = "kel.inscryption.myriadofJSON";
    public const string PluginName = "MyriadOfJSON";
    public const string PluginVersion = "1.0.0";
    
    internal static Plugin? Instance;
    
    /* configs! c: */
    // internal static ConfigEntry<bool>? Debug;

    private void Awake()
    {
        Instance = this; /* singleton! */ 
        Harmony harmony = new Harmony("kel.harmony.miscjson");
        harmony.PatchAll();

        LoadPelts.LoadAll();
        LoadMasks.LoadAll();
        LoadItems.LoadAll();

        LoadDebug(); /* TODO: remove */
        SerializeItemBase();
    }

    internal static void LogInfo(string message) => Instance?.Logger.LogInfo(message);
    internal static void LogError(string message) => Instance?.Logger.LogError(message);

    private static void LoadDebug()
    {
        DebugDummy.LoadDebugAbility();
    }

    private static void SerializeItemBase()
    {
        var item = new ItemJSONData()
        {
            customModelData = new(),
            actions = new()
            {
                drawCards = new DrawCardData[] { new() },
                scaleBalance = new ScaleBalanceData[] { new() },
                manageResources = new ManageResourcesData[] { new() },
                drawCardsFromPool = new DrawCardFromPoolData[] { new() },
                damageSlots = new DamageSlotsData[] { new() },
                placeCard = new PlaceCardData[] { new() },
                slotEffect = new SlotEffectData[] { new() },
                showMessage = new ShowMessageData[] { new() }
            }
        };
        FileLog.Log(JsonConvert.SerializeObject(item, Formatting.Indented));
    }
}

public class DebugDummy : AbilityBehaviour
{
    private static Ability ability = AbilityManager.New(
                Plugin.PluginGuid,
                "debug_dummy",
                "",
                typeof(DebugDummy),
                "dummy.png"
            ).ability;

    public override Ability Ability => ability; 

    public static void LoadDebugAbility()
    {
        CardLoader.GetCardByName("Kingfisher").AddAbilities(ability);
        CardLoader.GetCardByName("Bullfrog").AddAbilities(ability);
        CardLoader.GetCardByName("AntFlying").AddAbilities(ability);
        CardLoader.GetCardByName("DireWolf").AddAbilities(ability);
        // System.Console.WriteLine($"Ability: {dummy.Ability}");

    }
}
