using System.Collections.Generic;
using Exiled.API.Features;
using UnityEngine;

namespace BlinkFatigue
{
    public class Methods
    {
        private readonly Plugin plugin;
        public Methods(Plugin plugin) => this.plugin = plugin;

        public void SubtractTime(float value)
        {
            float newValue = plugin.SubtractTime -= value;
            plugin.SubtractTime = newValue >= 0f ? newValue : 0f;
        }

        public void AddTime(float value)
        {
            float newValue = plugin.SubtractTime += value;
            plugin.SubtractTime = newValue <= plugin.Config.MinTime ? newValue : plugin.Config.MinTime;
        }

        public void CustomBlinkSequence(Scp173PlayerScript playerScript)
        {
            if (!playerScript.isServer || !playerScript.isLocalPlayer)
                return;

            Scp173PlayerScript._remainingTime -= Time.fixedDeltaTime;
            Scp173PlayerScript._blinkTimeRemaining -= Time.fixedDeltaTime;

            if (Scp173PlayerScript._remainingTime >= 0f)
                return;

            Scp173PlayerScript._blinkTimeRemaining = playerScript.blinkDuration_see + 0.4f;
            Scp173PlayerScript._remainingTime = Mathf.Max(plugin.Config.MinBlinkTime, Random.Range(plugin.Config.MinTime, plugin.Config.MaxTime) - plugin.SubtractTime);

            if (plugin.SomeoneIsLooking)
            {
                float value = Random.Range(plugin.Config.AddMin, plugin.Config.AddMax);
                Log.Debug($"Adding {value} to {plugin.SubtractTime}");

                AddTime(value);
            }
            else
            {
                float value = Random.Range(plugin.Config.AddMin, plugin.Config.AddMax) * plugin.Config.DecreaseRate;
                Log.Debug($"Subtracting {value} from {plugin.SubtractTime}");
                SubtractTime(value);
            }
            
            foreach (Player player in Player.List)
                player.ReferenceHub.characterClassManager.Scp173.RpcBlinkTime();
        }
    }
}