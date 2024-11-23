using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : Interactable
{
    [SerializeField] protected List<Animator> animators;

    private bool isOpen = false;
    protected bool isAnimatorIdle;




    public override void Interact(InputAction.CallbackContext context) 
    {
        if(!isOutlined)
        {
            return;
        }

        isAnimatorIdle = true;
        foreach (var animator in animators)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("IdleClosed") && !animator.GetCurrentAnimatorStateInfo(0).IsName("IdleOpened"))
            {
                isAnimatorIdle = false;
                break;
            }
        }

        if (!isAnimatorIdle)
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
