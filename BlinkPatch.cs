using Harmony;
using UnityEngine;

namespace BlinkFatigue
{
	[HarmonyPatch(typeof(Scp173PlayerScript), "FixedUpdate")]
	public class BlinkPatchFixedUpdate
	{
		public static bool Prefix(Scp173PlayerScript __instance)
		{
			if (!BlinkFatigue.Instance.enabled) return true;

			BlinkCustomMethod.CustomBlinkingSequence(__instance);

			if (!__instance.iAm173 || (!__instance.isLocalPlayer && !Mirror.NetworkServer.active))
			{
				return false;
			}
			if (__instance._allowMove)
			{
				BlinkCustomMethod.reworkSubstractTime -= Time.fixedDeltaTime * BlinkConfigs.decreaseRate;
				if (BlinkCustomMethod.reworkSubstractTime < 0f)
				{
					BlinkCustomMethod.reworkSubstractTime = 0f;
				}
			}
			__instance._allowMove = true;
			foreach (GameObject gameObject in PlayerManager.players)
			{
				global::Scp173PlayerScript component = gameObject.GetComponent<global::Scp173PlayerScript>();
				if (!component.SameClass
					&& component.GetComponent<CharacterClassManager>().CurClass != RoleType.Tutorial
					&& component.LookFor173(__instance.gameObject, true)
					&& __instance.LookFor173(component.gameObject, false))
				{
					__instance._allowMove = false;
					break;
				}
			}
			return false;
		}
	}
}
