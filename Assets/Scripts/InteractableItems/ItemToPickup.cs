using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemToPickup : Interactable
{
    public override void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Picking up item");
    }
}
