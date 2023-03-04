using System;
using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Helpers;
using DiskCardGame;
using MyriadOfJSON.Parser.Names;

namespace MyriadOfJSON.Parser.Functions;
using WorldFunc = System.Func<string, MyriadOfJSON.Parser.NCalcBool>;

public static class WorldPredicates
{
    public static string[] Names =
    {
        FunctionNames.HasCardInHand,
        FunctionNames.HasCardInDeck,
        FunctionNames.IsCardOnBoard,
        FunctionNames.IsCardOnPlayerSide,
        FunctionNames.IsCardOnOpponentSide,
        FunctionNames.IsBoardEmpty,
        FunctionNames.IsSlotEmpty
        // TODO
    };

    public static Dictionary<string, WorldFunc> Functions = new()
    {
        { FunctionNames.HasCardInHand, HasCardInHand },
        { FunctionNames.HasCardInDeck, HasCardInDeck },
        { FunctionNames.IsCardOnBoard, IsCardOnBoard },
        { FunctionNames.IsCardOnPlayerSide, IsCardOnPlayerSide },
        { FunctionNames.IsCardOnOpponentSide, IsCardOnOpponentSide },
        { FunctionNames.IsBoardEmpty, IsBoardEmpty },
        { FunctionNames.IsSlotEmpty, IsSlotEmpty }
        // TODO
    };

    public static NCalcBool HasCardInHand (string cardName)
        => Singleton<PlayerHand>.Instance?.CardsInHand?.Any(x => x.Info.name == cardName) ?? false;

    public static NCalcBool HasCardInDeck (string cardName)
        => SaveManager.SaveFile.CurrentDeck?.Cards?.Any(x => x.name == cardName) ?? false;

    public static NCalcBool IsCardOnBoard (string cardName)
        => Singleton<BoardManager>.Instance?.CardsOnBoard?.Any(x => x.Info.name == cardName) ?? false;

    public static NCalcBool IsCardOnPlayerSide (string cardName)
        => Singleton<BoardManager>.Instance?.PlayerSlotsCopy
            ?.Where(x => x?.Card != null)
            ?.Any(x => x.Card.Info.name == cardName) ?? false;

    public static NCalcBool IsCardOnOpponentSide (string cardName)
        => Singleton<BoardManager>.Instance?.OpponentSlotsCopy
            ?.Where(x => x?.Card != null)
            ?.Any(x => x.Card.Info.name == cardName) ?? false;

    /* string param exists solely to comply with delegate! */
    public static NCalcBool IsBoardEmpty (string _)
        => Singleton<BoardManager>.Instance?.AllSlotsCopy?.All(x => x.Card == null) ?? true;

    public static NCalcBool IsSlotEmpty (string slot)
    {
        if (!int.TryParse(slot, out int slotIndex))
        {
            Plugin.LogError($"Invalid slot index: {slot ?? "(null)"}");
            return false;
        }
        return Singleton<BoardManager>.Instance?.AllSlotsCopy?.ElementAt(slotIndex)?.Card == null;
    }
}
