using System;
using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Helpers;
using DiskCardGame;

namespace MyriadOfJSON.Parser.Variables;

public static class VariableUtils
{
    public static int BoneAmount()
        => Singleton<ResourcesManager>.Instance?.PlayerBones ?? 0;

    public static int EnergyAmount()
        => Singleton<ResourcesManager>.Instance?.PlayerEnergy ?? 0;

    public static int MaxEnergy()
        => Singleton<ResourcesManager>.Instance?.PlayerMaxEnergy ?? 0;

    public static string OpponentName()
        => Singleton<TurnManager>.Instance?.Opponent?.ToString() ?? string.Empty;
    
    public static int TurnNumber()
        => Singleton<TurnManager>.Instance?.TurnNumber ?? 0;

    public static int ScaleBalance()
        => Singleton<LifeManager>.Instance?.Balance ?? 0;

    public static int FoilAmount()
        => RunState.Run?.currency ?? 0; 
}
