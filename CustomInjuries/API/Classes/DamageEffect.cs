using System.Collections.Generic;

using Exiled.API.Enums;

namespace CustomInjuries.API.Classes
{
    public class DamageEffect
    {
        public float DamageMultiplier { get; set; } = 1f;
        public float EffectsChance { get; set; } = 100f;

        public Dictionary<EffectType, float> Effects { get; set; } = new Dictionary<EffectType, float>
        {
            [EffectType.Bleeding] = 10f
        };
    }
}
