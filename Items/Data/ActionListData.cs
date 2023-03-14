using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public class ActionListData
{
    public DrawCardData[]? drawCards { get; set; }
    public ScaleBalanceData[]? scaleBalance { get; set; }
    public ManageResourcesData[]? manageResources { get; set; }
    public DrawCardFromPoolData[]? drawCardsFromPool { get; set; }
    public DamageSlotsData[]? damageSlots { get; set; }
    public PlaceCardData[]? placeCard { get; set; }
    public SlotEffectData[]? slotEffect { get; set; }
    public ShowMessageData[]? showMessage { get; set; }

    private IEnumerable<T>? CreateAll<T>(SortableActionData<T>[]? arr) where T : ActionBase
    {
        if (arr == null) return new T[0];
        for (int i = 0; i < arr.Length; i++)
        {
            /* Add internal indexes! (tiebreakers!) */
            arr[i].tiebreaker = i;
        }
        return arr.Select(x => x.Create());
    }

    public ActionList CreateActions(string itemName, string? condition)
    {
        List<ActionBase> actions = new();       

        /* i have to add each one like this. ><;;
         * if i find a better way, i'll refactor! */

        /* TODO: refactor! */
        actions.AddRange(CreateAll(drawCards));
        actions.AddRange(CreateAll(scaleBalance));
        actions.AddRange(CreateAll(manageResources));
        actions.AddRange(CreateAll(drawCardsFromPool));
        actions.AddRange(CreateAll(damageSlots));
        actions.AddRange(CreateAll(placeCard));
        actions.AddRange(CreateAll(slotEffect));
        actions.AddRange(CreateAll(showMessage));

        /* sort with icomparable! yay! c: */
        actions.Sort();
        return new(itemName, condition, actions);
    }
}
