using System;
using System.Linq;
using System.Collections;
using MyriadOfJSON.Items.Data;

namespace MyriadOfJSON.Items.Actions;

public abstract class ActionBase : IComparable<ActionBase>
{
    public int OrderIndex { get; private set; }
    public int Tiebreaker { get; private set; }

    /* set order! should always be used in constructor! */
    public void SetOrder<T>(SortableActionData<T> data)
    {
        OrderIndex = data.orderIndex;
        Tiebreaker = data.tiebreaker;
    }

    /* IComparable implementation for sorting! c: */
    public int CompareTo(ActionBase other)
    {
        if (this.OrderIndex == other.OrderIndex)
            return Tiebreaker.CompareTo(other.Tiebreaker);
        
        return OrderIndex.CompareTo(other.OrderIndex);
    }

    public abstract IEnumerator Trigger();
} 
