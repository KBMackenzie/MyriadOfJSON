using System.Text.RegularExpressions;
using DiskCardGame;

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

    protected abstract ChoiceType CardChoiceType { get; }
    protected BackupActionType BackupAction { get; set; } 

    protected CardSlot? SlotByIndex (int slotIndex)
        => ChooseSlot.GetSlots[CardChoiceType]().SafelyGet(slotIndex);
}
