using Newtonsoft.Json;
using System.Reflection;
using System;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace Bloodshed.Config
{
    public class BloodshedConfig
    {
        public string Version = BloodshedModSystem.Instance.Mod.Info.Version.ToString();
        public bool EnableStamina { get; set; } = true;
        public float MaxStamina { get; set; } = 100f;
        public float StaminaRegenSpeed { get; set; } = 1;
        
        // Costs
        public float SaturationCostPerStamina { get; set; } = 0.5f;
        public float DefenseStaminaCost { get; set; } = 0.02f;
        public float SwimStaminaCost { get; set; } = 0.05f;
        public float SprintStaminaCost { get; set; } = 0.05f;

        // Half Stamina Values
        public float WalkSpeedHalfStamina { get; set; } = 0.875f; // 87.5%
        public float MeleeAttackDamageHalfStamina { get; set; } = 0.9f; // 90%
        public float MeleeAttackSpeedHalfStamina { get; set; } = 0.9f; // 90%
        public float RangedAttackDamageHalfStamina { get; set; } = 0.9f; // 90%
        public float RangedAttackSpeedHalfStamina { get; set; } = 0.9f; // 90%
        public float RangedAttackAccuracyHalfStamina { get; set; } = 0.9f; // 90%

        // Exhausted Values
        public float WalkSpeedExhausted { get; set; } = 0.75f; // 75%
        public float MeleeAttackDamageExhausted { get; set; } = 0.7f; // 70%
        public float MeleeAttackSpeedExhausted { get; set; } = 0.7f; // 70%
        public float RangedAttackDamageExhausted { get; set; } = 0.7f; // 70%
        public float RangedAttackSpeedExhausted { get; set; } = 0.7f; // 70%
        public float RangedAttackAccuracyExhausted { get; set; } = 0.7f; // 70%

        public bool HideStaminaOnFull { get; set; } = true;
        public float StaminaCircleScale { get; set; } = 1f;
        public float StaminaCircleInnerRadius { get; set; } = 0.6f;
        public float StaminaCircleOuterRadius { get; set; } = 0.8f;
        public bool DebugMode { get; set; } = false;

    }
}
