using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Torch : RechargableItem
{
    [SerializeField] GameObject lightObject;

    protected override void StateItem(bool _bool)
    {
        lightObject.SetActive(_bool);
    }

    new private void OnDisable()
    {
        base.OnDisable();

        lightObject.SetActive(false);
        isOn = false;
    }
}
