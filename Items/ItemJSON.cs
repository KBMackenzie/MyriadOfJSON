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
    public string? description { get; set; }

    public int? powerLevel { get; set; }
    public bool? notRandomlyGiven { get; set; }

    public string? rulebookName { get; set; }
    public string? rulebookDescription { get; set; }
    public string? rulebookSprite { get; set; }

    /*internal ConsumableItemData GetData()
        => new()
        {
            description = description ?? string.Empty,
            powerLevel = powerLevel ?? 0,

            rulebookName = rulebookName ?? string.Empty,
            rulebookDescription = rulebookDescription ?? string.Empty,
            rulebookSprite = AssetHelpers.MakeSprite(rulebookSprite),
        };*/

    internal void CreateItem()
        => ConsumableItemManager.New(
                prefix,
                rulebookName ?? string.Empty,
                rulebookDescription ?? string.Empty,
                AssetHelpers.MakeTexture(rulebookSprite),
                typeof(DummyItem),
                ConsumableItemManager.ModelType.BasicRune
            ).SetAct1();
}