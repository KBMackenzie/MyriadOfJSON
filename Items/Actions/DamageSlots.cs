using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using DiskCardGame;
using MyriadOfJSON.Helpers;
using MyriadOfJSON.Parser;
using MyriadOfJSON.Items.Data;
using UnityEngine;
using NCalc;

namespace MyriadOfJSON.Items.Actions;
using ChoiceType = ChooseSlot.ChoiceType;

public class DamageSlots : SlotActionBase 
{
    protected override ChoiceType CardChoiceType => ChoiceType.All;

    public string[] Slots { get; }
    public string CardCondition { get; }
    public string AmountExpression { get; }

    public DamageSlots(DamageSlotsData data)
    {
        Slots = data.slots ?? new string[0];
        CardCondition = data.cardCondition ?? "true";
        AmountExpression = data.amountExpression ?? "0";
        BackupAction = Enum.TryParse(data.backupAction, out BackupActionType backup)
                        ? backup
                        : BackupActionType.AddToHand;
    }

    public override IEnumerator Trigger()
    {
        foreach (string slot in Slots)
        {
            yield return !ChoiceRegex.IsMatch(slot)
                ? ParseAsSlot(slot)
                : ChooseAndDamage();
        }
    }

    private CardSlot? ParseAsSlot(string? indexStr)
    {
        if (!int.TryParse(indexStr, out int amount))
            return null;

        return SlotByIndex(Mathf.Clamp(amount, 1, 8) - 1); 
    }

    private int EvaluateAmount(CardInfo card)
    {
        Expression? exp = ExpressionHandler.CardPredicate(CardCondition, card);
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
                    choice: ChoiceType.Player,
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
