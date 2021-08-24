using MelonLoader;
using MelonModManager.Util.Attributes;

namespace MelonModManager.Util
{
    public class MLMod: MelonMod
    {
        public string Description
        {
            get
            {
                var attribute =
                    Assembly.GetCustomAttributes(typeof(MelonDescriptionAttribute), false)[0] as
                        MelonDescriptionAttribute;
                return attribute?.Description;
            }
        }

        public override void OnApplicationStart()
        {
            HarmonyInstance.UnpatchAll();
        }

        public virtual void OnToggle(bool result) { }
        public virtual void OnSettingGUI() { }
    }
}