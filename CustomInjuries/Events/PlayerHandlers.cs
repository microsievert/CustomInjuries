using CustomInjuries.API.Classes;
using CustomInjuries.API.Classes.Extensions;
using CustomInjuries.API.Enums;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;

namespace CustomInjuries.Events
{
    public class PlayerHandlers
    {
        public void OnShot(ShotEventArgs ev)
        {
            if (ev.Target == null)
                return;

            if (CustomInjuries.Instance.Data.ImmunityPlayers.Contains(ev.Target))
                return;
        
            if (!ev.Target.IsHuman)
                return;

            if (ev.Target.IsGodModeEnabled)
                return;
            
            BoneType damagedBone = BoneTypeExtensions.GetByMassCenter(ev.Target.GameObject.transform.InverseTransformPoint(ev.Hitbox.CenterOfMass), ev.Hitbox._dmgMultiplier);
            ArmorType damagedArmor = ArmorTypeExtensions.GetHeaviestArmor(ev.Target);

            BoneParams boneParams = CustomInjuries.Instance.Config.BoneCustomInjuries[damagedBone];
            
            ev.Target.Hurt(boneParams.CalculateDamage(ev.Damage, damagedArmor, out bool instantKill));

            if (instantKill && ev.Target.Health < 250f)
                ev.Target.Kill(DamageType.Unknown);

            foreach (EffectType effect in boneParams.BoneHitEffects.Keys)
                ev.Target.EnableEffect(effect, boneParams.BoneHitEffects[effect]);

            if (ev.Target.CurrentItem == null || !CustomInjuries.Instance.Config.ItemLoseEnabled)
                return;
            
            if (UnityEngine.Random.Range(0, 100) >= CustomInjuries.Instance.Config.ItemLoseChance)
                return;

            ev.Target.DropItem(ev.Target.CurrentItem);
        }
    }
}