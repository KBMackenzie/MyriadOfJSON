namespace MyriadOfJSON.Parser.Names;

public static class FunctionNames
{
    // Card predicates:
    public const string HasTribe = "isOfTribe";
    public const string HasTrait = "hasTrait";
    public const string HasAbility = "hasAbility";
    public const string HasSpecialAbility = "hasSpecialAbility";
    public const string HasMetaCategory = "hasMetaCategory";
    public const string HasAppearanceBehaviour = "hasAppearance";
    public const string HasMoxCost = "hasMoxCost";

    // Card actions:
    public const string AddAbility = "addAbility";
    public const string AttackMod = "attackMod";
    public const string HealthMod = "healthMod";

    // World predicates:
    public const string HasCardInHand = "hasCardInHand";
    public const string HasCardInDeck = "hasCardInDeck";
    public const string IsCardOnBoard = "isCardOnBoard";
    public const string IsCardOnPlayerSide = "isCardOnPlayerSide";
    public const string IsCardOnOpponentSide = "isCardOnOpponentSide"; 
    public const string IsBoardEmpty = "isBoardEmpty";
    public const string IsSlotEmpty = "isSlotEmpty";
}
