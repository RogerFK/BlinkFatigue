using System.ComponentModel;
using Exiled.API.Interfaces;

namespace BlinkFatigue
{
    public class Config : IConfig
    {
        [Description("Whether or not the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }

        public float DecreaseRate { get; set; } = 0.75f;
        public float MinBlinkTime { get; set; } = 1.5f;
        public float MinTime { get; set; } = 2.5f;
        public float MaxTime { get; set; } = 3.5f;
        public float AddMin { get; set; } = 0.35f;
        public float AddMax { get; set; } = 0.45f;
    }
}