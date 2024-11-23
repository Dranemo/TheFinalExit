using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigDoorOpening : Door
{
    [SerializeField] List<GameObject> colliders;
    public override void Interact(InputAction.CallbackContext context)
    {
        base.Interact(context);

        if(!isOutlined)
        {
            return;
        }

        if(!isAnimatorIdle)
        {
            return;
        }

        foreach (var collider in colliders)
        {
            collider.SetActive(!collider.activeSelf);
        }
    }
}
