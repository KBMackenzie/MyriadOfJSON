using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Pelts;
using MyriadOfJSON.Helpers;

namespace MyriadOfJSON.Pelts;

public class PeltData
{
    // The name of an existing card to make into a pelt.
    public string? cardName { get; set; }
    public int? basePrice { get; set; }
    public int? extraAbilitiesToAdd { get; set; }
    public int? choicesOfferedByTrader { get; set; }

    public string? condition { get; set; }

    // Additional parameters to make a modder's life easier:
    public bool? allowRareCards { get; set; }
    public bool? allowGiantCards { get; set; }
    public bool? onlyAllowTraderChoice { get; set; }

    // Implicitly adds "Trait.Pelt" and "SpecialTriggeredAbility.SpawnLice".
    public void MakePelt()
    {
        CardInfo? x = CardHelpers.Get(cardName);
        if (x == null) return;

        x.AddTraits(Trait.Pelt);
        x.AddSpecialAbilities(SpecialTriggeredAbility.SpawnLice);

        PeltManager.New
        (
            pluginGuid: Plugin.PluginGuid,
            peltCardInfo: x,
            baseBuyPrice: basePrice ?? 2,
            extraAbilitiesToAdd: extraAbilitiesToAdd ?? 0,
            choicesOfferedByTrader: choicesOfferedByTrader ?? 8,
            getCardChoices: () => PeltParser.ParseCardChoices(this)
        );
    }
}
