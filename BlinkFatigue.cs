using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EXILED;
using Harmony;

namespace BlinkFatigue
{
    public class BlinkFatigue : EXILED.Plugin
    {
        public static BlinkFatigue Instance { private set; get; }
        private BlinkEvents BlinkEvents;
        public override string getName => "BlinkFatigue";
        public bool enabled = false;

        // Bool used to check if there was an error. 
        // If there was, then act as if this plugin wasn't enabled since, otherwise, you wouldn't be guaranteed it works.
        internal bool Available { set; get; }
        public static HarmonyInstance HarmonyInstance { set; get; }
        public override void OnDisable()
        {
            enabled = false;
            Events.WaitingForPlayersEvent -= this.BlinkEvents.OnWaitingForPlayers;
        }
        public override void OnEnable()
        {
            enabled = true;
            if (Instance == null)
            {
                Instance = this;
                this.BlinkEvents = new BlinkEvents();
                Available = false;

                HarmonyInstance = HarmonyInstance.Create("rogerfk.exiled.blinkfatigue");
                HarmonyInstance.PatchAll();
            }

            Events.WaitingForPlayersEvent += this.BlinkEvents.OnWaitingForPlayers;

        }

        public override void OnReload()
        {
            BlinkConfigs.ReloadConfigs();
        }
    }
}
