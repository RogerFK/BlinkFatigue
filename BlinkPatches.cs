using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using UnityEngine;
using EXILED;

namespace BlinkFatigue
{
	[HarmonyPatch(typeof(Scp173PlayerScript), "FixedUpdate")]
	public class BlinkPatchFixedUpdate
	{
		public static bool Prefix(Scp173PlayerScript __instance)
		{
			Plugin.Info("bruh");
			if (!BlinkFatigue.Instance.enabled || !BlinkFatigue.Instance.Available) return true;

			BlinkStuff.CustomBlinkingSequence(__instance);
			
			if (!__instance.iAm173 || (!__instance.isLocalPlayer && !Mirror.NetworkServer.active))
			{
				return false;
			}
			if (BlinkStuff.allowMove)
			{
				BlinkStuff.reworkPlusTime -= Time.fixedDeltaTime * 2f;
				if (BlinkStuff.reworkPlusTime < 0f)
				{
					BlinkStuff.reworkPlusTime = 0f;
				}
			}
			BlinkStuff.allowMove = true;
			foreach (GameObject gameObject in global::PlayerManager.players)
			{
				global::Scp173PlayerScript component = gameObject.GetComponent<global::Scp173PlayerScript>();
				if (!component.SameClass 
					&& (bool) BlinkStuff.LookFor173.Invoke(component,  new object[] { __instance.gameObject, true })
					&& (bool) BlinkStuff.LookFor173.Invoke(__instance, new object[] { component.gameObject, true }))
				{
					BlinkStuff.allowMove = false;
					break;
				}
			}
			BlinkStuff._allowMove.SetValue(__instance, BlinkStuff.allowMove);
			return false;
		}
	}
}
