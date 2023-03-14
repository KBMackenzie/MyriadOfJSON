using System.Collections;
using DiskCardGame;
using MyriadOfJSON.Items.Data;
using MyriadOfJSON.Parser;
using NCalc;

namespace MyriadOfJSON.Items.Actions;
using ChoiceType = ChooseSlot.ChoiceType;

public class DamageSlots : SlotActionBase 
{
    public string[] Slots { get; }
    public string CardCondition { get; }
    public string AmountExpression { get; }

    public DamageSlots(DamageSlotsData data)
    {
        Slots = data.slots ?? new string[0];
        CardCondition = data.cardCondition ?? "true";
        AmountExpression = data.amountExpression ?? "0";

        /*BackupAction = data.ParseBackupAction(
                    defaultAction: BackupActionType.DoNothing);*/

        CardChoiceType = data.ParseChoiceType(
                    defaultChoice: ChoiceType.All);
        SetOrder(data);
    }

    public override IEnumerator Trigger()
    {
        foreach (string slot in Slots)
        {
            yield return !ChoiceRegex.IsMatch(slot)
                ? ParseAndDamage(slot)
                : ChooseAndDamage();
        }
    }

    private int EvaluateAmount(CardInfo card)
    {
        Expression? exp = ExpressionHandler.CardPredicate(AmountExpression, card);
        return ExpressionHandler.SafelyParseAsInt(exp) ?? 0;
    }

    private IEnumerator DamageSlot(CardSlot slot, int amount)
    {
        yield return slot?.Card.TakeDamage(amount, null);
    }

    private IEnumerator ParseAndDamage(string? slotStr)
    {
        CardSlot? slot = ParseAsSlot(slotStr);
        if (slot?.Card == null) yield break;
        int amount = EvaluateAmount(slot.Card.Info);
        yield return DamageSlot(slot, amount);
    }

    private IEnumerator ChooseAndDamage()
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
        yield return DamageSlot(
                    slot: chooseSlot.Target,
                    amount: EvaluateAmount(chooseSlot.Target.Card.Info)
                );
        yield break;
    }
} 
