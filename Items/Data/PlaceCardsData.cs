using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public class PlaceCardData : SlotActionBaseData<PlaceCard>
{
    public string? card { get; set; }
    public string? slot { get; set; }
    public string? choiceCondition { get; set; }
    public bool? canReplace { get; set; }

    public override PlaceCard Create()
        => new(this);
}
