using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemToPickup : Interactable
{
    Inventory inv;

    private void Start()
    {
        inv = FindObjectOfType<Inventory>();
    }


    public override void Interact(InputAction.CallbackContext context)
    {
        inv.AddItem(gameObject);
    }
}
