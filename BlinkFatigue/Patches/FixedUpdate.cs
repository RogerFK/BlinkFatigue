using Exiled.API.Features;
using HarmonyLib;

namespace BlinkFatigue.Patches
{
    [HarmonyPatch(typeof(Scp173PlayerScript), nameof(Scp173PlayerScript.FixedUpdate))]
    public class FixedUpdate
    {
        public static bool Prefix(Scp173PlayerScript __instance)
        {
            Plugin.Singleton.Functions.CustomBlinkSequence(__instance);

            if (!__instance.iAm173 || (!__instance.isLocalPlayer && !Mirror.NetworkServer.active))
                return false;

            __instance.AllowMove = true;
            Plugin.Singleton.SomeoneIsLooking = false;

            foreach (Player player in Player.List)
            {
                Scp173PlayerScript playerScript = player.ReferenceHub.characterClassManager.Scp173;

                if (!playerScript.SameClass && player.Role != RoleType.Tutorial &&
                    playerScript.LookFor173(__instance.gameObject, true) &&
                    __instance.LookFor173(player.GameObject, false))
                {
                    __instance.AllowMove = false;
                    Plugin.Singleton.SomeoneIsLooking = true;
                    break;
                }
            }
            
            if (!Plugin.Singleton.SomeoneIsLooking) 
                Plugin.Singleton.Functions.SubtractTime(UnityEngine.Time.fixedDeltaTime * Plugin.Singleton.Config.DecreaseRate);

            return false;
        }
    }
}