using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Par exemple, la touche E pour ouvrir/fermer le tiroir
        {
            animator.SetBool("IsOpen", isOpen);
            animator.SetTrigger("Interacts");

            isOpen = !isOpen;
        }
    }
}
