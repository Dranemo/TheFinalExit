using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : Interactable
{
    [SerializeField] protected List<Animator> animators;

    [SerializeField] AudioClip doorOpeningSound;

    [SerializeField] AudioClip openingOnAnItemSound;
    [SerializeField] SpawningItemScript spawningItemScript;

    private bool isOpen = false;
    protected bool isAnimatorIdle;




    public override void Interact(InputAction.CallbackContext context) 
    {

        if (!isOutlined)
        {
            return;
        }

        base.Interact(context);
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
        AudioManager.Instance.PlaySound(doorOpeningSound, transform.position);

        if(spawningItemScript != null)
        {
            if(!spawningItemScript.GetHasMadeSoundWhenOpened())
            {
                AudioManager.Instance.PlaySound(openingOnAnItemSound, transform.position);
                spawningItemScript.HasMadeSound();
            }
        }


        foreach (var animator in animators)
        {
            Door door = animator.GetComponent<Door>();
            if(door != this) 
            {
                door.isOpen = isOpen;
            }

            animator.SetBool("IsOpen", isOpen);
            animator.SetTrigger("Interacts");
        }

    }
}
