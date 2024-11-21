using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : Interactable
{
    [SerializeField] List<Animator> animators;

    private bool isOpen = false;




    public override void Interact(InputAction.CallbackContext context) 
    {
        if(!isOutlined)
        {
            return;
        }


        Debug.Log("Interacting with door"); 
        isOpen = !isOpen;

        foreach (var animator in animators)
        {
            animator.SetBool("IsOpen", isOpen);
            animator.SetTrigger("Interacts");
        }

    }
}
