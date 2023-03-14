using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using MyriadOfJSON.Parser;
using MyriadOfJSON.Helpers;
using NCalc;

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
    public string CardCondition { get; }
    public bool AllowEmptySlots { get; }
    public bool AllowFullSlots { get; }

    public CardSlot? Target { get; private set; }
    private readonly View DefaultView = View.Board;

    public static Dictionary<ChoiceType, SlotListFunc> GetSlots = new()
    {
        { ChoiceType.All, () => Singleton<BoardManager>.Instance.AllSlotsCopy }, 
        { ChoiceType.Player, () => Singleton<BoardManager>.Instance.PlayerSlotsCopy }, 
        { ChoiceType.Opponent, () => Singleton<BoardManager>.Instance.OpponentSlotsCopy }, 
    };

    public List<CardSlot> GetValidSlots()
        => GetSlots[Choice]().Where(Predicate).ToList(); 

    public bool HasValidSlots()
        => GetValidSlots().Count > 0;

    public ChooseSlot(string? choiceType, string? cardCondition, bool? allowEmptySlots, bool? allowFullSlots)
    {
        Choice = Enum.TryParse(choiceType?.SentenceCase(), out ChoiceType choice)
                    ? choice
                    : ChoiceType.All;
        CardCondition = cardCondition ?? "true";
        AllowEmptySlots = allowEmptySlots ?? false; 
        AllowFullSlots = allowFullSlots ?? false;
    }

    public ChooseSlot(ChoiceType choice, string? cardCondition, bool? allowEmptySlots, bool? allowFullSlots)
    {
        Choice = choice;
        CardCondition = cardCondition ?? "true";
        AllowEmptySlots = allowEmptySlots ?? false;
        AllowFullSlots = allowFullSlots ?? false;
    }

    public bool Predicate(CardSlot slot)
    {
        if (slot.Card == null) return AllowEmptySlots;
        if (!AllowFullSlots) return false;

        Expression? exp = ExpressionHandler.CardPredicate(CardCondition, slot.Card.Info);
        return ExpressionHandler.SafeEvaluation(exp);
    }

    public IEnumerator Choose(Action<CardSlot>? onInvalidTarget = null)
    {
        List<CardSlot> allSlots = GetSlots[Choice]();
        List<CardSlot> validSlots = GetValidSlots();

        Singleton<ViewManager>.Instance.SwitchToView(DefaultView, false, false);
        Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

        yield return Singleton<BoardManager>.Instance?.ChooseTarget(
                    allSlots,
                    validSlots,
                    (x) => Target = x,
                    onInvalidTarget,
                    null,
                    null,
                    CursorType.Point
                );
        yield break;
    }

    /* Adding this as the 5th argument for Singleton<BoardManager>.Instance?.ChooseTarget() ...
     * () => Singleton<ViewManager>.Instance.CurrentView != DefaultView,
     * ... would allow for 'cancelling' the choice process by looking away from the board.
     * I feel it's not really necessary with JSON items right now. ><;; 
     * It would take a lot to implement. Not worth it. I just gotta make sure there are
     * no 'locks' when choosing slots and being able to cancel the action. */

    public void CardAction(Action<CardSlot> fn)
    {
        if (Target == null) return;
        Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
        Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
        fn(Target);
    }

    public void End()
    {
        Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
    }
}
