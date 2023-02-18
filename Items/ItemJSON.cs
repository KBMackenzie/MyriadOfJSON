using System;
using System.Linq;
using System.Collections.Generic;
using InscryptionAPI.Items;
using DiskCardGame;
using UnityEngine;
using InscryptionAPI.TalkingCards.Helpers;
using InscryptionAPI.Items.Extensions;

namespace MiscellaneousJSON.Items;

public class ItemJSON
{
    public string? prefix { get; set; }
    
    public string? name { get; set; }
    public string? description { get; set; }
    public string? rulebookTexture { get; set; }

    public int? powerLevel { get; set; }
    public bool? notRandomlyGiven { get; set; }

    internal void CreateItem()
        => ConsumableItemManager.New(
                prefix,
                name ?? string.Empty,
                description ?? string.Empty,
                AssetHelpers.MakeTexture(rulebookTexture),
                typeof(DummyItem),
                ConsumableItemManager.ModelType.BasicRune
            ).SetPowerLevel(powerLevel ?? 0)
            .SetNotRandomlyGiven(notRandomlyGiven ?? false)
            .SetAct1();
}