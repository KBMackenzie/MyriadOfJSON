using System;
using System.Linq;
using System.Collections.Generic;
using MyriadOfJSON.Items.Actions;

namespace MyriadOfJSON.Items.Data;

public class SlotEffectData : SlotActionBaseData<SlotEffect>
{
    public string? slot { get; set; }
    public string[]? callbacks { get; set; }
    public string? cardCondition { get; set; }

    public override SlotEffect Create()
        => new(this);
}
