using System;
using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Helpers;
using DiskCardGame;
using System.Collections;

namespace MyriadOfJSON.Items.Actions;

public class PlaceCards : ActionBase
{
    public class PlaceInfo 
    {
        public string? Card { get; }
        public string Slot { get; }
        public string ChoiceCondition { get; }
        
        public PlaceInfo(string card, string? slot, string? choiceCondition)
        {
            Card = card;
            Slot = slot ?? "[Choose]";
            ChoiceCondition = choiceCondition ?? "true";
        }
    }

    public PlaceInfo[] CardsToPlace { get; }

    public PlaceCards(PlaceInfo[]? cardsToPlace)
    {
        CardsToPlace = cardsToPlace ?? new PlaceInfo[0];
    }

    public override IEnumerator Trigger()
    {
        throw new NotImplementedException();
    }
}
