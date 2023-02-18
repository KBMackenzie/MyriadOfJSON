using InscryptionAPI.Masks;
using DiskCardGame;
using MiscellaneousJSON.Helpers;

namespace MiscellaneousJSON.Masks;

public class MaskData
{
    public string? prefix { get; set; }
    public string? maskName { get; set; }
    public string? overrideMask { get; set; }
    public string? maskTexture { get; set; }

    private LeshyAnimationController.Mask ParseMask()
        => AssetHelpers.ParseAsEnumValue<LeshyAnimationController.Mask>(overrideMask);

    public void MakeMask()
        => MaskManager.Override(prefix, maskName, ParseMask(), maskTexture);
}
