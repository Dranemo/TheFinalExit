using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemToPickup : Interactable
{
    Inventory inv;
    public ItemSpawner.ItemType itemType;

    private void Start()
    {
        inv = Inventory.Instance();
    }


    public override void Interact(InputAction.CallbackContext context)
    {
        if(isOutlined)
            inv.AddItem(transform.parent.gameObject);
    }

    public ItemSpawner.ItemType GetItemType()
    {
        return itemType;
    }
}
