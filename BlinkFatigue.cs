using EXILED;
using Harmony;

namespace BlinkFatigue
{
    public class BlinkFatigue : EXILED.Plugin
    {
        public static BlinkFatigue Instance { private set; get; }
        public override string getName => "BlinkFatigue";
        public bool enabled = false;

        public static HarmonyInstance HarmonyInstance { set; get; }
        public override void OnDisable()
        {
            if (enabled == false)
            {
                Plugin.Error("BlinkFatigue was already disabled.");
                return;
            }

            enabled = false;

            Plugin.Info("Disabled BlinkFatigue.");
        }
        public override void OnEnable()
        {
            enabled = Config.GetBool("blink_enable", true);

            if (enabled == false)
            {
                Plugin.Error("BlinkFatigue is disabled via config. Check your configs.");
                return;
            }
            if (Instance == null)
            {
                Instance = this;
                HarmonyInstance = HarmonyInstance.Create($"rogerfk.exiled.blinkfatigue");

                HarmonyInstance.PatchAll();
            }

            BlinkConfigs.ReloadConfigs();

            Plugin.Info("Enabled BlinkFatigue.");
        }

        public override void OnReload()
        {
            enabled = Config.GetBool("blink_enable", true);
            
            if (Instance == null && enabled)
            {
                OnEnable();
            }
            else BlinkConfigs.ReloadConfigs();

            Plugin.Info("Reloaded BlinkFatigue.");
        }
    }
}
