using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public abstract class SlotActionBaseData<T> : SortableActionData<T> where T : SlotActionBase
{
    public string? backupAction { get; set; }
}
