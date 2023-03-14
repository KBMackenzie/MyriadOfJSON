using System;
using MyriadOfJSON.Helpers;
using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;
using ChoiceType = ChooseSlot.ChoiceType;
using BackupActionType = SlotActionBase.BackupActionType;

public abstract class SlotActionBaseData<T> : SortableActionData<T> where T : SlotActionBase
{
    public string? backupAction { get; set; }
    public string? cardChoiceType { get; set; }

    public ChoiceType ParseChoiceType(ChoiceType defaultChoice = ChoiceType.All)
       => Enum.TryParse(cardChoiceType?.SentenceCase(), out ChoiceType choice)
            ? choice
            : defaultChoice;

    public BackupActionType ParseBackupAction(BackupActionType defaultAction = BackupActionType.DoNothing)
       => Enum.TryParse(backupAction?.SentenceCase(), out BackupActionType backup)
            ? backup
            : defaultAction; 
}
