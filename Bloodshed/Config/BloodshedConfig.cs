using Vintagestory.API.MathTools;

namespace Bloodshed.Config
{
    public class BloodshedConfig
    {
        public bool EnableStamina { get; set; } = true;
        public float MaxStamina { get; set; } = 100f;
        public int StaminaRegenSpeed { get; set; } = 1;
        
        public float SaturationCostPerStamina { get; set; } = 0.5f;
        public float DefenseStaminaCost { get; set; } = 0.02f;
        public float SwimStaminaCost { get; set; } = 0.05f;
        public float SprintStaminaCost { get; set; } = 0.05f;

        public float WalkSpeedReductionHalfStamina { get; set; } = 0.125f;
        public float WalkSpeedReductionExhausted { get; set; } = 0.25f;

        public bool HideStaminaOnFull { get; set; } = true;
        public float StaminaCircleScale { get; set; } = 1f;
        public float StaminaCircleInnerRadius { get; set; } = 0.6f;
        public float StaminaCircleOuterRadius { get; set; } = 0.8f;
        public bool DebugMode { get; set; } = false;
    }
}
