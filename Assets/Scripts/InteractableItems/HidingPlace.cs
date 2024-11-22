using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingPlace : Interactable
{
    [SerializeField] GameObject virtualCam;



    private void Start()
    {
        virtualCam.SetActive(false);

    }


    public override void Interact(InputAction.CallbackContext context)
    {
        if(isOutlined)
        {
            virtualCam.SetActive(!virtualCam.activeSelf);
            Debug.Log("Interacting with hiding place");
        }
    }
}
