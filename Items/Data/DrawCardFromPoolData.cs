using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public class DrawCardFromPoolData : SortableActionData<DrawCardFromPool>
{
    public string? cardCondition { get; set; }
    public int? cardAmount { get; set; }
    public string[]? callbacks { get; set; }
    public bool? allowRareCards { get; set; }

    public override DrawCardFromPool Create()
        => new(cardCondition, cardAmount, callbacks, allowRareCards);
}
