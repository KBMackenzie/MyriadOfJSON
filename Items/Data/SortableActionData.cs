namespace MyriadOfJSON.Items.Data;

public abstract class SortableActionData<T> 
{
    public int orderIndex { get; set; }
    public int tiebreaker { get; set; }

    /* Properties for basic sorting! 
     * Will be passed to the actual ActionBase instances. */

    public abstract T Create();
}
