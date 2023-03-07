using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public class ManageResourcesData : SortableActionData<ManageResources>
{
    public string? resourceType { get; set; } 
    public string? expression { get; set; }

    public override ManageResources Create()
        => new(resourceType, expression);
}

