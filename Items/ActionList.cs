using MyriadOfJSON.Items.Actions;
using System.Collections.Generic;

namespace MyriadOfJSON.Items;

/* A list of actions for an item! */
public class ActionList
{
    public string ItemName { get; }
    public string? Condition { get; }

    public readonly List<ActionBase> Actions = new();

    public ActionList(string itemName, string? condition, IEnumerable<ActionBase>? actions = null)
    {
        ItemName = itemName;
        Condition = condition;
        if (actions != null) Actions.AddRange(actions);
    }
}
