using DiskCardGame;

namespace MyriadOfJSON.Items.Actions;
using ChoiceType = ChooseSlot.ChoiceType;

public abstract class SlotChoiceActionBase : ActionBase
{
    public const string Choose = "[choose]";
    public const string AllSlots = "[all]";

    public enum BackupActionType
    {
        DoNothing,
        AddToHand
    }

    protected abstract ChoiceType CardChoiceType { get; }
    protected BackupActionType BackupAction { get; set; } 

    protected CardSlot SlotByIndex (int slotIndex)
        => ChooseSlot.GetSlots[CardChoiceType]()[slotIndex - 1];
}
