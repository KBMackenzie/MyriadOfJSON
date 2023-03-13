using System;
using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Helpers;
using DiskCardGame;
using MyriadOfJSON.Parser.Functions;
using MyriadOfJSON.Items.Data;
using System.Collections;

namespace MyriadOfJSON.Items.Actions;
using ChoiceType = ChooseSlot.ChoiceType;

public class SlotEffect : SlotActionBase
{
    public string Slot { get; }
    public string[] Callbacks { get; }
    public string CardCondition { get; }

    public SlotEffect(SlotEffectData data)
    {
        Slot = data.slot ?? "choose";
        Callbacks = data.callbacks ?? new string[0];
        CardCondition = data.cardCondition ?? "true";

        BackupAction = data.ParseBackupAction(
                defaultAction: BackupActionType.DoNothing); 

        CardChoiceType = data.ParseChoiceType(
                defaultChoice: ChoiceType.All);
        SetOrder(data);
    }

    public override IEnumerator Trigger()
    {
        yield return ChoiceRegex.IsMatch(Slot)
            ? ChooseAndCall()
            : ParseAndCall();
    }

    private void ApplyCallbacks(PlayableCard card)
        => AsCardAction.ParseAllFunctions(card, Callbacks);

    private IEnumerator ParseAndCall()
    {
        CardSlot? slot = ParseAsSlot(Slot);
        if (slot?.Card == null) yield break;
        ApplyCallbacks(slot.Card);
    }

    private IEnumerator ChooseAndCall()
    {
        ChooseSlot chooseSlot = new(
                    choice: CardChoiceType, 
                    cardCondition: CardCondition, 
                    allowEmptySlots: false,
                    allowFullSlots: true 
                );
        if (!chooseSlot.HasValidSlots()) 
            yield break;
        yield return chooseSlot.Choose();
        if (chooseSlot.Target?.Card == null)
            yield break;
        ApplyCallbacks(chooseSlot.Target.Card);
        yield break;
    }
}
