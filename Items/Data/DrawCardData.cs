using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public class DrawCardData : SortableActionData<DrawCard>
{
    public string[]? cardNames { get; set; }
    public string[]? callbacks { get; set; }

    public override DrawCard Create()
        => new(this);
}
