using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public class ScaleBalanceData : SortableActionData<ScaleBalance>
{
    public string? expression { get; set; }
    public bool? toPlayer { get; set; }

    public override ScaleBalance Create()
        => new(this);
}
