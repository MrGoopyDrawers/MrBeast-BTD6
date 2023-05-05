using MelonLoader;
using BTD_Mod_Helper;
using mrbeast;

[assembly: MelonInfo(typeof(mrbeast.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace mrbeast;

public class Main : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<Main>("mrbeast loaded!");
    }
}