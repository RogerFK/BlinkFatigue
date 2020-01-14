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
        private static uint bruhCounter = 0;
        public override void OnDisable()
        {
            enabled = false;
            EventPlugin.Scp173PatchDisable = false;
            HarmonyInstance.UnpatchAll();

            Plugin.Info("Disabled BlinkFatigue.");
        }
        public override void OnEnable()
        {
            enabled = true;
            EventPlugin.Scp173PatchDisable = true;
            if (Instance == null)
            {
                Instance = this;
            }
            BlinkConfigs.ReloadConfigs();
            bruhCounter++;
            HarmonyInstance = HarmonyInstance.Create($"rogerfk.exiled.blinkfatigue{bruhCounter}");
            HarmonyInstance.PatchAll();

            Plugin.Info("Enabled BlinkFatigue.");
        }

        public override void OnReload()
        {
            BlinkConfigs.ReloadConfigs();

            Plugin.Info("Reloaded BlinkFatigue.");
        }
    }
}
