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
    public bool AllowEmptySlots { get; }
    public string CardCondition { get; }
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

    public ChooseSlot(string? choiceType, string? cardCondition, bool? allowEmptySlots)
    {
        Choice = Enum.TryParse(choiceType?.SentenceCase(), out ChoiceType choice)
                    ? choice
                    : ChoiceType.All;
        CardCondition = cardCondition ?? "true";
        AllowEmptySlots = allowEmptySlots ?? false; 
    }

    public ChooseSlot(ChoiceType choice, string? cardCondition, bool? allowEmptySlots)
    {
        Choice = choice;
        CardCondition = cardCondition ?? "true";
        AllowEmptySlots = allowEmptySlots ?? false;
    }

    public bool Predicate(CardSlot slot)
    {
        if (!AllowEmptySlots && slot?.Card == null)
            return false;

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
                    () => Singleton<ViewManager>.Instance.CurrentView != DefaultView,
                    CursorType.Point
                );
        yield break;
    }

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
