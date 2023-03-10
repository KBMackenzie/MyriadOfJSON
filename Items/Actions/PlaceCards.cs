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
    public class PlaceInfo 
    {
        public string? Card { get; }
        public string Slot { get; }
        public string ChoiceCondition { get; }
        
        public PlaceInfo(string card, string? slot, string? choiceCondition)
        {
            Card = card;
            Slot = slot ?? "[Choose]";
            ChoiceCondition = choiceCondition ?? "true";
        }
    }

    public enum BackupAction
    {
        DoNothing,
        AddToHand
    }

    public PlaceInfo[] CardsToPlace { get; }
    public BackupAction Backup { get; }

    public PlaceCards(PlaceInfo[]? cardsToPlace, string? backupAction)
    {
        CardsToPlace = cardsToPlace ?? new PlaceInfo[0];
        Backup = Enum.TryParse(backupAction, out BackupAction backup)
                    ? backup
                    : BackupAction.AddToHand;
    }

    public override IEnumerator Trigger()
    {
        foreach (PlaceInfo placeInfo in CardsToPlace)
        {
            if (placeInfo.Slot.ToLower() == "[choose]" || !int.TryParse(placeInfo.Slot, out int slot))
            {
                yield return ChooseAndPlace(placeInfo);
                continue;
            }
            
        }
    }

    private IEnumerator PlaceCardInSlot(PlaceInfo placeInfo, int slotIndex)
    {
        slotIndex = Mathf.Clamp(slotIndex, 1, 4);
        CardSlot slot = ChooseSlot.GetSlots[ChooseSlot.ChoiceType.Player]()[slotIndex];
        // TODO: place card in board
        yield break;
    }

    private IEnumerator ChooseAndPlace(PlaceInfo placeInfo)
    {
        CardInfo? card = CardHelpers.Get(placeInfo.Card);
        if (card == null) yield break;
        ChooseSlot chooseSlot = new(
                    ChooseSlot.ChoiceType.Player,
                    placeInfo.ChoiceCondition,
                    true        
                );
        if (!chooseSlot.HasValidSlots()) 
        {
            yield return DoBackupAction(card);
            yield break;
        };
        yield return chooseSlot.Choose();
        // TODO: place card in board
        yield break;
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
