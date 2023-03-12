using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public class PlaceCardsData : SlotActionBaseData<PlaceCards>
{
    public string? card { get; set; }
    public string? slot { get; set; }
    public string? choiceCondition { get; set; }
    public bool? canReplace { get; set; }

    public override PlaceCards Create()
        => new(this);
}
