using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkFatigue
{
    public static class BlinkConfigs
    {
        internal static float decreaseRate;
        internal static float maxBlinkTime;
        internal static float minBlinkTime;
        internal static float minReworkBlinkTime;

        internal static void ReloadConfigs()
        {
            decreaseRate = EXILED.Plugin.Config.GetFloat("blink_decreaserate");
            minReworkBlinkTime = EXILED.Plugin.Config.GetFloat("blink_minreworktime", 1f);
            minBlinkTime = EXILED.Plugin.Config.GetFloat("blink_mintime", 2.5f);
            maxBlinkTime = EXILED.Plugin.Config.GetFloat("blink_maxtime", 3.5f);
        }
    }
}
