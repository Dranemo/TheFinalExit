using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemToPickup : Interactable
{
    [SerializeField] AudioClip takeSound;
    Inventory inv;
    public ItemSpawner.UsableItemType itemType;

    private void Start()
    {
        inv = Inventory.Instance();
    }


    public override void Interact(InputAction.CallbackContext context)
    {
        if(isOutlined)
        {
            inv.AddItem(transform.parent.gameObject);
            AudioManager.Instance.PlaySound(takeSound, transform.position);
        }
    }

    public ItemSpawner.UsableItemType GetItemType()
    {
        return itemType;
    }
}
