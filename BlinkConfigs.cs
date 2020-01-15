namespace BlinkFatigue
{
    public static class BlinkConfigs
    {
        internal static float decreaseRate;
        internal static float maxBlinkTime;
        internal static float minBlinkTime;
        internal static float minReworkBlinkTime;
        internal static float reworkAddMin;
        internal static float reworkAddMax;

        internal static void ReloadConfigs()
        {
            decreaseRate = EXILED.Plugin.Config.GetFloat("blink_decreaserate", 0.75f);
      minReworkBlinkTime = EXILED.Plugin.Config.GetFloat("blink_minblinktime", 1.5f);
            minBlinkTime = EXILED.Plugin.Config.GetFloat("blink_mintime", 2.5f);
            maxBlinkTime = EXILED.Plugin.Config.GetFloat("blink_maxtime", 3.5f);
            reworkAddMin = EXILED.Plugin.Config.GetFloat("blink_addmin", 0.22f);
            reworkAddMax = EXILED.Plugin.Config.GetFloat("blink_addmax", 0.34f);
        }
    }
}
