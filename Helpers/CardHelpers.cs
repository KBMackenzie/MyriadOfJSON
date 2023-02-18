using DiskCardGame;
using System;

#nullable enable
namespace MiscellaneousJSON.Helpers; 

internal class CardHelpers
{
    public static CardInfo? Get(string? name)
    {
        CardInfo? card;
        try
        {
            card = CardLoader.GetCardByName(name);
        }
        catch (Exception)
        {
            Plugin.LogError($"Couldn't find a card of name {name ?? "(null)"}!");
            return null;
        }
        return card;
    }
}
