﻿using CustomInjuries.API.Classes;
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

            ev.Damage = boneParams.CalculateDamage(ev.Damage, damagedArmor, out bool instantKill);

            if (instantKill && ev.Target.Health < 250f)
                ev.Damage = ev.Target.Health;

            foreach (EffectType effect in boneParams.BoneHitEffects.Keys)
                ev.Target.EnableEffect(effect, boneParams.BoneHitEffects[effect]);

            if (ev.Target.CurrentItem == null || !CustomInjuries.Instance.Config.ItemLoseEnabled)
                return;
            
            if (UnityEngine.Random.Range(0, 100) >= CustomInjuries.Instance.Config.ItemLoseChance)
                return;

            ev.Target.DropItem(ev.Target.CurrentItem);
        } 

        public void OnHurting(HurtingEventArgs ev)
        {
            if (!CustomInjuries.Instance.Config.DamageCustomEffects.ContainsKey(ev.Handler.Type))
                return;

            DamageEffect damageEffect = CustomInjuries.Instance.Config.DamageCustomEffects[ev.Handler.Type];

            ev.Handler.Damage *= damageEffect.DamageMultiplier;

            if (damageEffect.EffectsChance == 0)
                return;

            if (UnityEngine.Random.Range(0, 100) >= damageEffect.EffectsChance)
                return;

            foreach (EffectType effect in damageEffect.Effects.Keys)
                ev.Target.EnableEffect(effect, damageEffect.Effects[effect]);
        }
    }
}