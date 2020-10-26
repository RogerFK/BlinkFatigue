using System;
using Exiled.API.Features;
using HarmonyLib;

namespace BlinkFatigue
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "Galaxy119";
        public override string Name { get; } = "BlinkFatigue";
        public override string Prefix { get; } = "BlinkFatigue";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 9);

        public static Plugin Singleton;
        
        public Methods Functions { get; private set; }
        public Harmony Harmony;

        public bool SomeoneIsLooking { get; set; }
        public float SubtractTime = 0f;

        public override void OnEnabled()
        {
            Singleton = this;
            Functions = new Methods(this);
            Harmony = new Harmony($"com.galaxy.blinkfatigue-{DateTime.Now.Ticks}");
            
            Harmony.PatchAll();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Functions = null;
            Harmony.UnpatchAll();
            Harmony = null;

            base.OnDisabled();
        }
    }
}