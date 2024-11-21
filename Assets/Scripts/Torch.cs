using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Torch : RechargableItem
{
    [SerializeField] GameObject lightObject;

    private void Start()
    {
        itemType = Inventory.ItemType.Torch;
    }

    protected override void StateItem(bool _bool)
    {
        lightObject.SetActive(_bool);
    }






}
