using System;
using System.Linq;
using System.Collections.Generic;
using MiscellaneousJSON.Helpers;
using DiskCardGame;

namespace MiscellaneousJSON.Parser.Functions;
using WorldFunc = System.Func<string, MiscellaneousJSON.Parser.NCalcBool>;

public static class WorldPredicates
{
    public static string[] Names =
    {
        // TODO
    };

    public static Dictionary<string, WorldFunc> Functions = new()
    {
        // TODO
    };

    public static NCalcBool HasCardInHand (string cardName)
        => Singleton<PlayerHand>.Instance?.CardsInHand?.Any(x => x.Info.name == cardName) ?? false;

    public static NCalcBool HasCardInDeck (string cardName)
        => SaveManager.SaveFile.CurrentDeck?.Cards?.Any(x => x.name == cardName) ?? false;

    public static NCalcBool CardIsOnBoard (string cardName)
        => Singleton<BoardManager>.Instance?.CardsOnBoard?.Any(x => x.Info.name == cardName) ?? false;

    public static NCalcBool HasCardOnPlayerSide (string cardName)
        => Singleton<BoardManager>.Instance?.PlayerSlotsCopy
            ?.Where(x => x?.Card != null)
            ?.Any(x => x.Card.Info.name == cardName) ?? false;

    public static NCalcBool HasCardOnOpponentSide (string cardName)
        => Singleton<BoardManager>.Instance?.OpponentSlotsCopy
            ?.Where(x => x?.Card != null)
            ?.Any(x => x.Card.Info.name == cardName) ?? false;
}
