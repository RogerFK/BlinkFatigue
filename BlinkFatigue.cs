using EXILED;
using Harmony;

namespace BlinkFatigue
{
    public class BlinkFatigue : EXILED.Plugin
    {
        public static HarmonyInstance HarmonyInstance { set; get; }
        private static uint harmonyCounter = 0;
        public const string Version = "1.0.1";
        public override string getName => "BlinkFatigue";
        public bool enabled = false;
        public override void OnDisable()
        {
            if (enabled == false)
            {
                return;
            }

            enabled = false;
            HarmonyInstance.UnpatchAll();
            Plugin.Info("Disabled BlinkFatigue.");
        }
        public override void OnEnable()
        {
            enabled = Config.GetBool("blink_enable", true);

            if (enabled == false)
            {
                Plugin.Info("BlinkFatigue is disabled via config. Check your configs if you think this is an error.");
                return;
            }

            HarmonyInstance = HarmonyInstance.Create($"rogerfk.exiled.blinkfatigue{harmonyCounter}");
            HarmonyInstance.PatchAll();

            BlinkConfigs.ReloadConfigs();

            Plugin.Info($"Enabled BlinkFatigue v{Version}.");
        }

        public override void OnReload()
        {
            // Unused, this only has to be used when dealing with variables that you'll need after changing assemblies without restarting the server.
            Plugin.Info("Reloading BlinkFatigue to its newest version.");
        }
    }
}
