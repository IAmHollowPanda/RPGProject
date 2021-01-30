using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : AnimationController{

    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment,AnimationClip[]> weaponAnimationsDictionary;

    protected override void Start(){
        base.Start();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        weaponAnimationsDictionary = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations item in weaponAnimations)
        {
            weaponAnimationsDictionary.Add(item.weapon, item.clips);
        }
    }
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem){

        if(newItem != null && newItem.equipSlot == EquipmentSlot.Weapon){
            animator.SetLayerWeight(1, 1);
            if(weaponAnimationsDictionary.ContainsKey(newItem)){
                currentAttackAnimationSet = weaponAnimationsDictionary[newItem];
            }
        }else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon){
            animator.SetLayerWeight(1, 0);
            currentAttackAnimationSet = defaultAttackAnimationSet;
        }

        if(newItem != null && newItem.equipSlot == EquipmentSlot.Shield){
            animator.SetLayerWeight(2, 1);
        }else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield){
            animator.SetLayerWeight(2, 0);
        }
    }

    [System.Serializable]
    public struct WeaponAnimations{
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
