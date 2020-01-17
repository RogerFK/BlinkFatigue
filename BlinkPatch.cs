using Harmony;
using UnityEngine;

namespace BlinkFatigue
{
	[HarmonyPatch(typeof(Scp173PlayerScript), "FixedUpdate")]
	public class BlinkPatchFixedUpdate
	{
		[HarmonyPriority(420)]
		public static bool Prefix(Scp173PlayerScript __instance)
		{
			BlinkCustomMethod.CustomBlinkingSequence(__instance);
			
			if (!__instance.iAm173 || (!__instance.isLocalPlayer && !Mirror.NetworkServer.active))
			{
				return false;
			}
			if (!BlinkCustomMethod.someoneLooking)
			{
				BlinkCustomMethod.reworkSubstractTime -= Time.fixedDeltaTime * BlinkConfigs.decreaseRate;
				if (BlinkCustomMethod.reworkSubstractTime < 0f)
				{
					BlinkCustomMethod.reworkSubstractTime = 0f;
				}
			}
			__instance._allowMove = true;
			BlinkCustomMethod.someoneLooking = false;
			foreach (GameObject gameObject in PlayerManager.players)
			{
				Scp173PlayerScript component = gameObject.GetComponent<Scp173PlayerScript>();
				if (!component.SameClass
					&& component.GetComponent<CharacterClassManager>().CurClass != RoleType.Tutorial
					&& component.LookFor173(__instance.gameObject, true)
					&& __instance.LookFor173(component.gameObject, false))
				{
					__instance._allowMove = false;
					BlinkCustomMethod.someoneLooking = true;
					break;
				}
			}
			return false;
		}
	}
}
