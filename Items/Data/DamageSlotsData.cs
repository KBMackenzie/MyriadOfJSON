using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public class DamageSlotsData : SlotActionBaseData<DamageSlots>
{
    public string[]? slots { get; set; }
    public string? cardCondition { get; set; }
    public string? amountExpression { get; set; }

    public override DamageSlots Create()
        => new(this);
}
