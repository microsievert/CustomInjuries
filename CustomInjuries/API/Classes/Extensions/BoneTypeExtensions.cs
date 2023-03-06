using System;

using UnityEngine;

using CustomInjuries.API.Enums;

namespace CustomInjuries.API.Classes.Extensions
{
    public static class BoneTypeExtensions
    {
        public static BoneType GetByMassCenter(Vector3 boneLocalMassCenter, HitboxType hitboxType)
        {
            switch (hitboxType)
            {
                case HitboxType.Headshot:
                    return BoneType.Head;
                case HitboxType.Body:
                    if (IsArms(boneLocalMassCenter))
                        return IsRight(boneLocalMassCenter) ? BoneType.RightHand : BoneType.LeftHand;
                    
                    return BoneType.Body;
                case HitboxType.Limb:
                    bool isRight = IsRight(boneLocalMassCenter);

                    return isRight ? BoneType.RightLeg : BoneType.LeftLeg;
                default:
                    return BoneType.Unknown;
            }
        }

        private static bool IsArms(Vector3 point) => point.z <= -0.2 || Math.Abs(point.x) > 0.1;

        private static bool IsRight(Vector3 point) => point.x > 0;
    }
}