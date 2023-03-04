using System;
using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Helpers;
using DiskCardGame;
using NCalc;
using MyriadOfJSON.Parser.Names;

namespace MyriadOfJSON.Parser.Variables;

public static class MakeVariables
{
    public static void CardVariables (ref Expression exp, CardInfo card)
    {
        exp.Parameters[VarNames.BloodCost] = card.BloodCost;
        exp.Parameters[VarNames.BoneCost] = card.BonesCost;
        exp.Parameters[VarNames.EnergyCost] = card.EnergyCost;
        exp.Parameters[VarNames.Temple] = card.temple.ToString();

        exp.Parameters[VarNames.Name] = card.name;
        exp.Parameters[VarNames.DisplayedName] = card.displayedName;
    }

    public static void WorldVariables (ref Expression exp)
    {

    }
}
