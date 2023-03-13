using System;
using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public class ShowMessageData : SortableActionData<ShowMessage>
{
    public string? message { get; set; }
    public float? duration { get; set; }
    public string? emotion { get; set; }
    public string? letterAnimation { get; set; }
    public bool? waitForInput { get; set; }

    public override ShowMessage Create()
        => new(this);
}
