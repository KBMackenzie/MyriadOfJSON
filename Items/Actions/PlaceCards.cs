using System;
using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Helpers;
using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace MyriadOfJSON.Items.Actions;

public class PlaceCards : ActionBase
{
    public const string Choose = "[choose]";

    public enum BackupAction
    {
        DoNothing,
        AddToHand
    }

    public string? Card { get; }
    public string Slot { get; }
    public string ChoiceCondition { get; }
    public bool CanReplace { get; }

    public BackupAction Backup { get; }

    public PlaceCards(string? card, string? slot, string? choiceCondition, bool? canReplace,
            string? backupAction)
    {
        Card = card;
        Slot = slot ?? Choose;
        ChoiceCondition = choiceCondition ?? "true";
        CanReplace = canReplace ?? false;
        Backup = Enum.TryParse(backupAction, out BackupAction backup)
                    ? backup
                    : BackupAction.AddToHand;
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

    private CardSlot SlotByIndex (int slotIndex)
        => ChooseSlot.GetSlots[ChooseSlot.ChoiceType.Player]()[slotIndex - 1];

    private IEnumerator ChooseAndPlace()
    {
        CardInfo? card = CardHelpers.Get(Card);
        if (card == null) yield break;
        ChooseSlot chooseSlot = new(
                    choice: ChooseSlot.ChoiceType.Player,
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
        if (Backup == BackupAction.DoNothing || card == null)
            yield break;

        if (Backup == BackupAction.AddToHand)
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
