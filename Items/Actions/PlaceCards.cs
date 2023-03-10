using System;
using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Helpers;
using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace MyriadOfJSON.Items.Actions;
using ChoiceType = ChooseSlot.ChoiceType;

public class PlaceCards : SlotActionBase 
{
    public string? Card { get; }
    public string Slot { get; }
    public string ChoiceCondition { get; }
    public bool CanReplace { get; }

    protected override ChoiceType CardChoiceType => ChoiceType.Player; 

    public PlaceCards(string? card, string? slot, string? choiceCondition, bool? canReplace,
            string? backupAction)
    {
        Card = card;
        Slot = slot ?? Choose;
        ChoiceCondition = choiceCondition ?? "true";
        CanReplace = canReplace ?? false;
        BackupAction = Enum.TryParse(backupAction, out BackupActionType backup)
                    ? backup
                    : BackupActionType.AddToHand;
    }

    public override IEnumerator Trigger()
    {
        if (Slot.ToLower() == Choose || !int.TryParse(Slot, out int slot))
        {
            yield return ChooseAndPlace();
            yield break;
        } 
        yield return PlaceCardInSlot(slot);
    }

    private IEnumerator PlaceCardInSlot(int slotIndex)
    {
        CardInfo? card = CardHelpers.Get(Card);
        if (card == null) yield break;
        slotIndex = Mathf.Clamp(slotIndex, 1, 4);
        CardSlot slot = SlotByIndex(slotIndex);
        if (!CanReplace && slot.Card != null)
        {
            yield return DoBackupAction(card);
            yield break;
        }
        yield return Place(card, slot);
        yield break;
    }

    private IEnumerator ChooseAndPlace()
    {
        CardInfo? card = CardHelpers.Get(Card);
        if (card == null) yield break;
        ChooseSlot chooseSlot = new(
                    choice: ChoiceType.Player,
                    cardCondition: ChoiceCondition,
                    allowEmptySlots: true,
                    allowFullSlots: CanReplace 
                );
        if (!chooseSlot.HasValidSlots()) 
        {
            yield return DoBackupAction(card);
            yield break;
        };
        yield return chooseSlot.Choose();
        if (chooseSlot.Target == null)
        {
            yield return DoBackupAction(card);
            yield break;
        }
        yield return Place(card, chooseSlot.Target);
        yield break;
    }

    private IEnumerator Place(CardInfo card, CardSlot slot)
    {
        if (slot.Card != null)
            yield return slot.Card.Die(false);

        yield return Singleton<BoardManager>.Instance.CreateCardInSlot(
                    info: card,
                    slot: slot,
                    transitionLength: 0.15f,
                    resolveTriggers: true
                );
    }

    private IEnumerator DoBackupAction(CardInfo? card = null)
    {
        if (BackupAction == BackupActionType.DoNothing || card == null)
            yield break;

        if (BackupAction == BackupActionType.AddToHand)
        {
            yield return DrawCard(card);
            yield break;
        }
    }

    private IEnumerator DrawCard (CardInfo card)
    {
        Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, false);
        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(
                info: card,
                temporaryMods: null
            );
        yield return new WaitForSeconds(0.45f); 
    }
}
