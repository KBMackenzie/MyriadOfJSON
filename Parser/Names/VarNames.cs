namespace MyriadOfJSON.Parser.Names;

public static class VarNames
{
    /* World variables: */
    public const string BoneAmount = "BoneAmount";
    public const string MaxEnergy = "MaxEnergy";
    public const string EnergyAmount = "BoneAmount";
    public const string OpponentName = "OpponentName";
    public const string TurnNumber = "TurnNumber";
    public const string ScaleDamage = "ScaleDamage";
    /* scale damage is negative when scale is tipped against player! */
    public const string FoilAmount = "FoilAmount"; 

    /* Card-specific variables: */
    public const string BloodCost = "BloodCost";
    public const string BoneCost = "BoneCost";
    public const string EnergyCost = "EnergyCost";
    public const string Temple = "Temple";
    public const string Name = "InternalName";
    public const string DisplayedName = "DisplayedName";
}
