using DiskCardGame;
using NCalc;
using MyriadOfJSON.Parser.Names;

#pragma warning disable Publicizer001
namespace MyriadOfJSON.Parser.Variables;

public static class MakeVariables
{
    public static void CardVariables (Expression exp, CardInfo card)
    {
        exp.Parameters[VarNames.BloodCost] = card.BloodCost;
        exp.Parameters[VarNames.BoneCost] = card.BonesCost;
        exp.Parameters[VarNames.EnergyCost] = card.EnergyCost;
        exp.Parameters[VarNames.Temple] = card.temple.ToString();

        exp.Parameters[VarNames.Name] = card.name;
        exp.Parameters[VarNames.DisplayedName] = card.displayedName;
    }

    public static void WorldVariables (Expression exp)
    {
        exp.Parameters[VarNames.BoneAmount] = VariableUtils.BoneAmount();
        exp.Parameters[VarNames.MaxEnergy] = VariableUtils.MaxEnergy();
        exp.Parameters[VarNames.EnergyAmount] = VariableUtils.EnergyAmount();
        exp.Parameters[VarNames.OpponentName] = VariableUtils.OpponentName();
        exp.Parameters[VarNames.TurnNumber] = VariableUtils.TurnNumber();
        exp.Parameters[VarNames.ScaleDamage] = VariableUtils.ScaleBalance();
        exp.Parameters[VarNames.FoilAmount] = VariableUtils.FoilAmount();
    }

    public static void SlotVariables (Expression exp, CardSlot slot)
    {
        exp.Parameters[VarNames.SlotCardName] = VariableUtils.SlotCardName(slot);
    }
}
