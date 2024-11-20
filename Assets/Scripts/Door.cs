using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] List<Animator> animators;
    [SerializeField] List<GameObject> itemSpawningPoints;
    [SerializeField] List<GameObject> prefabItemsToSpawn;

    [SerializeField] Outline outline;
    [SerializeField] GameObject buttonDisplay;


    [SerializeField] InputActionReference interact;

    private bool isOpen = false;
    private bool isOutlined = false;

    public void SetIsOutlined(bool _isOutlined)
    {
        if(_isOutlined == isOutlined)
        {
            return;
        }


        isOutlined = _isOutlined;
        outline.OutlineWidth = (_isOutlined ? 6 : 0);
        buttonDisplay.SetActive(_isOutlined);
    }


    private void OnEnable()
    {
        interact.action.Enable();
        interact.action.performed += Interact;
    }

    private void OnDisable()
    {
        interact.action.Disable();
        interact.action.performed -= Interact;
    }




    private void Start()
    {
        outline.OutlineWidth = 0;
        buttonDisplay.SetActive(false);
    }




    public void Interact(InputAction.CallbackContext context)
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
