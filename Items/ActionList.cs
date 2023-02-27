﻿using MiscellaneousJSON.Items.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiscellaneousJSON.Items;

public class ActionList
{
    public string ItemName { get; }
    public string? Condition { get; }

    public List<ActionBase> Actions = new();

    public ActionList(string itemName, string? condition, IEnumerable<ActionBase>? actions = null)
    {
        ItemName = itemName;
        Condition = condition;
        if (actions != null) Actions.AddRange(actions);
    }
}