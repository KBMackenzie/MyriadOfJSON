using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;
public class AllActionData
{
    public DrawCardsData[]? drawCards { get; set; }
    public ScaleBalanceData[]? scaleBalance { get; set; }
    public ManageResourcesData[]? manageResources { get; set; }
    public DrawCardFromPoolData[]? drawCardsFromPool { get; set; }

    private IEnumerable<T>? CreateAll<T>(SortableActionData<T>[]? arr)
    {
        if (arr == null) return null;
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
        
        return new(itemName, condition, actions);
    }
}
