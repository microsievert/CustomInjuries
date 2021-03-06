using System.Collections.Generic;
using System.ComponentModel;
using CustomInjuries.API.Classes;
using CustomInjuries.API.Enums;
using Exiled.API.Interfaces;

namespace CustomInjuries
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Dice damage customization (including body armor damage customization)")]
        public Dictionary<BoneType, BoneParams> BoneCustomInjuries { get; set; } = new Dictionary<BoneType, BoneParams>
        {
            [BoneType.Head] = new BoneParams(),
            [BoneType.Body] = new BoneParams(),
            [BoneType.LeftHand] = new BoneParams(),
            [BoneType.RightHand] = new BoneParams(),
            [BoneType.RightLeg] = new BoneParams(),
            [BoneType.LeftLeg] = new BoneParams()
        };

        [Description("Can player lost item if someone damage his hands")]
        public bool ItemLoseEnabled { get; set; } = false;

        [Description("After what level of health the player loses weapons from getting hit into the hands")]
        public float ItemLoseChance { get; set; } = 25f;
    }
}