using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;


    public Transform Shield;
    public Transform Sword;

    void Awake() {
        instance = this;
    }
    #endregion

    Inventory inventory;

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;


    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    void Start(){
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        
        Equipment oldItem = Unequip(slotIndex);


        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, null);
        }

        SetEquipmentBlendShapes(newItem, 100);
        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        currentMeshes[slotIndex] = newMesh;

        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            newMesh.rootBone = Sword;
        }else if(newItem != null && newItem.equipSlot == EquipmentSlot.Shield)        
        {
            newMesh.rootBone = Shield;
        }else
        {
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;
        }


    }

    public Equipment Unequip (int slotIndex){

        if(currentEquipment[slotIndex]!=null){
            
            if(currentMeshes[slotIndex]!=null){
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);

            inventory.AddItem(oldItem);
            currentEquipment[slotIndex] = null;

            if(onEquipmentChanged != null){
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    public void UnequipAll(){
        for(int i=0; i<currentEquipment.Length; i++){
            Unequip(i);
        }

        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight){
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems(){
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.U)){
            UnequipAll();
        }
    }
}
