using System.Collections.Generic;
using System.Text.RegularExpressions;
using DiskCardGame;
using UnityEngine;

namespace MyriadOfJSON.Items.Actions;
using ChoiceType = ChooseSlot.ChoiceType;

public abstract class SlotActionBase : ActionBase
{
    public readonly Regex ChoiceRegex = new(
                pattern: @"^\[?choose\]?$",
                options: RegexOptions.IgnoreCase
            ); 

    public enum BackupActionType
    {
        DoNothing,
        AddToHand
    }

    protected ChoiceType CardChoiceType { get; set; }
    protected BackupActionType BackupAction { get; set; } 

    protected CardSlot? SlotByIndex (int slotIndex)
        => ChooseSlot.GetSlots[CardChoiceType]().SafelyGet(slotIndex);

    public static Dictionary<ChoiceType, int> SlotMaximum = new()
    {
        { ChoiceType.All, 8 },
        { ChoiceType.Player, 4 },
        { ChoiceType.Opponent, 4 }
    };

    /* Assumes indexing always starts at 1 for JSON items! */
    protected CardSlot? ParseAsSlot(string? indexStr)
    {
        if (!int.TryParse(indexStr, out int amount))
            return null;

        int slotMax = SlotMaximum[CardChoiceType];
        return SlotByIndex(Mathf.Clamp(amount, 1, slotMax) - 1); 
    }

}
