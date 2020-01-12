using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using Harmony;

namespace BlinkFatigue
{
	internal static class BlinkStuff
	{
		// Variables used by the plugin
		private static float blinkTimeRemaining;
		private static float remainingTime;
		private static float blinkDuration_see;

		internal static float reworkPlusTime = 0f;
		internal static bool allowMove = false;

		// Reflection methods/fields, needed for private methods
		internal static bool alreadyRan = false;

		internal static FieldInfo _allowMove;
		internal static MethodInfo LookFor173;

		private static FieldInfo _blinkTimeRemaining { set; get; }
		private static MethodInfo RpcBlinkTime;

		internal static void InitializeAndFetchAllValues(Scp173PlayerScript script)
		{
            alreadyRan = true;
			var s = typeof(Scp173PlayerScript);

            // Needed for extra-server checks
            _blinkTimeRemaining = s.GetField("_blinkTimeRemaining", BindingFlags.NonPublic | BindingFlags.Static);
			_allowMove = s.GetField("_allowMove", BindingFlags.NonPublic | BindingFlags.Instance);
			
			// Needed to make the player blink
			RpcBlinkTime = s.GetMethod("RpcBlinkTime", BindingFlags.NonPublic | BindingFlags.Instance);
			LookFor173 = s.GetMethod("LookFor173", BindingFlags.NonPublic | BindingFlags.Instance);

			// Needed to to be faithful to the core game's anti-cheat
			FieldInfo tempDurField = s.GetField("blinkDuration_see");
            if (_blinkTimeRemaining == null || _allowMove == null || RpcBlinkTime == null || LookFor173 == null || tempDurField == null)
            {
                BlinkFatigue.Instance.Available = false;
                BlinkFatigue.Error("BlinkFatigue is OUTDATED. Please, check the GitHub to watch for a new release or notify RogerFK about it.");
                BlinkFatigue.Error(string.Format("**ONLY IF YOU'RE USING THE LATEST UPDATE OF THE PLUGIN** Give him this info: {0} {1} {2}",
                    _blinkTimeRemaining == null ? "_blinkTimeRemaining is null." : "\b",
					_allowMove == null ? "_allowMove is null." : "\b",
					RpcBlinkTime == null ? "RpcBlinkTime is null." : "\b",
					LookFor173 == null ? "LookFor173 is null." : "\b",
                    tempDurField == null ? "blinkDuration_see has changed." : "\b"));
			}
            else
            {
				try
				{
					blinkDuration_see = (float)tempDurField.GetValue(script);
				}
				catch (Exception e)
				{
					EXILED.Plugin.Error("Reading field blinkDuration_see threw this exception:\n" + e);

					BlinkFatigue.Instance.Available = false;
					return;
				}
                BlinkFatigue.Instance.Available = true;
				/*var harmonyPatch = HarmonyInstance.Create("rogerfk.exiled.blinkfatigue");

				// Harmony Patch
				harmonyPatch.PatchAll();*/
				EXILED.Plugin.Info("Succesfully loaded BlinkFatigue");
			}
		}
		internal static void CustomBlinkingSequence(Scp173PlayerScript scpScript)
		{
			if (!scpScript.isServer || !scpScript.isLocalPlayer)
			{
				return;
			}
			remainingTime -= Time.fixedDeltaTime;

			if (blinkTimeRemaining != -1f)
			{
				if (blinkTimeRemaining <= 0f)
				{
					_blinkTimeRemaining.SetValue(null, 0f);
					blinkTimeRemaining = -1f;
				}
				else
				{
					blinkTimeRemaining -= Time.fixedDeltaTime;
				}
			}

			if (remainingTime >= 0f)
			{
				return;
			}

			blinkTimeRemaining = blinkDuration_see + 0.5f;
			_blinkTimeRemaining.SetValue(null, blinkTimeRemaining);
			remainingTime = Mathf.Max(BlinkConfigs.minReworkBlinkTime, UnityEngine.Random.Range(BlinkConfigs.minBlinkTime, BlinkConfigs.maxBlinkTime) - reworkPlusTime);
			if (!allowMove)
			{
				reworkPlusTime += UnityEngine.Random.Range(0.25f, 0.45f);
				// If SCP-173 is sick of your shit, this basically negates an infinite stacking of the blink fatigue ability
				if (reworkPlusTime > BlinkConfigs.minBlinkTime)
				{
					reworkPlusTime = BlinkConfigs.minBlinkTime;
				}
			}
			var array = PlayerManager.players;
			for (int i = 0; i < array.Count; i++)
			{
				var comp = array[i].GetComponent<Scp173PlayerScript>();
				if (comp != null)
				{
					RpcBlinkTime.Invoke(comp, null);
				}
			}
		}
	}
}
