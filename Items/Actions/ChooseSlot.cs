using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using UnityEngine;
using MyriadOfJSON.Parser;
using MyriadOfJSON.Helpers;
using InscryptionAPI.Card;

namespace MyriadOfJSON.Items.Actions;
using SlotListFunc = System.Func<System.Collections.Generic.List<DiskCardGame.CardSlot>>;

/* Helper for choosing slots! */
public class ChooseSlot
{
    public enum ChoiceType
    {
        All,
        Player,
        Opponent
    }

    public ChoiceType Choice { get; }
    public string? SlotCondition { get; }
    public string? CardCondition { get; }
    public CardSlot? Target { get; private set; }
    private readonly View DefaultView = View.Board;

    public Dictionary<ChoiceType, SlotListFunc> GetSlots = new()
    {
        { ChoiceType.All, () => Singleton<BoardManager>.Instance.AllSlotsCopy }, 
        { ChoiceType.Player, () => Singleton<BoardManager>.Instance.PlayerSlotsCopy }, 
        { ChoiceType.Opponent, () => Singleton<BoardManager>.Instance.OpponentSlotsCopy }, 
    };

    public List<CardSlot> GetValidSlots()
        => GetSlots[Choice]().Where(Predicate).ToList(); 

    public ChooseSlot(string? choiceType, string? cardCondition, string? slotCondition)
    {
        Choice = EnumHelpers.TryParse(choiceType, out ChoiceType choice) ? choice : ChoiceType.All;
        CardCondition = cardCondition;
        SlotCondition = slotCondition;
    }

    public bool Predicate(CardSlot cardSlot)
    {
        // TODO
        return true;
    }

    public IEnumerator Choose(Action<CardSlot>? onInvalidTarget = null)
    {
        List<CardSlot> allSlots = GetSlots[Choice]();
        List<CardSlot> validSlots = GetValidSlots();

        Singleton<ViewManager>.Instance?.SwitchToView(DefaultView, false, false);
        Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        Singleton<ViewManager>.Instance!.Controller.LockState = ViewLockState.Unlocked;

        yield return Singleton<BoardManager>.Instance?.ChooseTarget(
                    allSlots,
                    validSlots,
                    (x) => Target = x,
                    onInvalidTarget,
                    null,
                    () => Singleton<ViewManager>.Instance.CurrentView != DefaultView,
                    CursorType.Point
                );
        yield break;
    }

    public IEnumerable CardAction(Action<CardSlot> action)
    {
        if (Target == null) yield break;
        Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
        Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
        action(Target);
    }

    public void End()
    {
        Singleton<ViewManager>.Instance!.Controller.LockState = ViewLockState.Unlocked;
    }
}
