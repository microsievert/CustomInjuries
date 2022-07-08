using CustomInjuries.API.Enums;
using UnityEngine;

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
                    return BoneType.Body;
                case HitboxType.Limb:
                    bool isRight = IsRight(boneLocalMassCenter);
                    bool isArms = IsArms(boneLocalMassCenter);

                    if (isArms)
                        return isRight ? BoneType.RightHand : BoneType.LeftHand;

                    return isRight ? BoneType.RightLeg : BoneType.LeftLeg;
                default:
                    return BoneType.Unknown;
            }
        }

        private static bool IsArms(Vector3 point) => point.y > 0.15f;

        private static bool IsRight(Vector3 point) => point.x > 0;
    }
}