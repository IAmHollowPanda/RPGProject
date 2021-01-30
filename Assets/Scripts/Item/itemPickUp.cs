using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickUp : Interactable
{
    public Item item;
    
    public override void Interact(){
        base.Interact();

        PickUp();
    }

    void PickUp(){
        
        Debug.Log(item.name + " stored");
        bool wasPickedUp = Inventory.instance.AddItem(item);

        if(wasPickedUp==true)
            Destroy(gameObject);
    }
}
