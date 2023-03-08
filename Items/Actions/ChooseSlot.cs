using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using UnityEngine;
using MyriadOfJSON.Parser;
using InscryptionAPI.Card;

namespace MyriadOfJSON.Items.Actions;

/* Helper for choosing slots! */
public class ChooseSlot
{
    public CardSlot? Target { get; private set; }
    private readonly View DefaultView = View.Board;

    public IEnumerator Choose(List<CardSlot> validSlots, Action<CardSlot>? onInvalidTarget = null)
    {
        Singleton<ViewManager>.Instance?.SwitchToView(DefaultView, false, false);
        Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        Singleton<ViewManager>.Instance!.Controller.LockState = ViewLockState.Unlocked;

        yield return Singleton<BoardManager>.Instance?.ChooseTarget(
                    Singleton<BoardManager>.Instance.AllSlotsCopy,
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
}
