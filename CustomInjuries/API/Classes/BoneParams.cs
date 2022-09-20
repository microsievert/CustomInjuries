using System.Collections.Generic;
using System.ComponentModel;

using Exiled.API.Enums;

using CustomInjuries.API.Enums;

namespace CustomInjuries.API.Classes
{
    public class BoneParams
    {
        [Description("Damage multiplier to a given bone (without body armor)")]
        public float DamageFactor { get; set; } = 1;

        [Description("Instant death from hit in this limb (without body armor)")] 
        public bool InstantKill { get; set; } = false;
        
        [Description("Effects on hitting the bone")]
        public Dictionary<EffectType, float> BoneHitEffects { get; set; } = new Dictionary<EffectType, float>
        {
            [EffectType.Burned] = 1f
        };
        
        [Description("Body armor bone hit settings.")]
        public Dictionary<ArmorType, float> ArmorsDamageFactor { get; set; } = new Dictionary<ArmorType, float>
        {
            [ArmorType.LightArmor] = 1,
            [ArmorType.CombatArmor] = 1,
            [ArmorType.HeavyArmor] = 1
        };
        
        public Dictionary<ArmorType, bool> ArmorsInstantKill { get; set; } = new Dictionary<ArmorType, bool>
        {
            [ArmorType.LightArmor] = false,
            [ArmorType.CombatArmor] = false,
            [ArmorType.HeavyArmor] = false
        };

        public float CalculateDamage(float damage, ArmorType armor, out bool instantKill)
        {
            float calculatedDamage = damage;

            if (armor != ArmorType.NoArmor)
            {
                calculatedDamage *= ArmorsDamageFactor[armor];
                instantKill = ArmorsInstantKill[armor];
            }
            else
            {
                calculatedDamage *= DamageFactor;
                instantKill = InstantKill;
            }

            return calculatedDamage;
        }
    }
}