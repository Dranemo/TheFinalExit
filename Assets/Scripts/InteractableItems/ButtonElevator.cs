using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonElevator : Interactable
{
    [SerializeField] bool isInElevator = false;
    [SerializeField] AudioClip  buttonSound;


    bool buttonFound = false;

    PlayerStats playerStats;
    private void Start()
    {
        playerStats = PlayerStats.Instance();
    }



    public override void Interact(InputAction.CallbackContext context)
    {

        if (!isOutlined)
        {
            return;
        }
        base.Interact(context);
        AudioManager.Instance.PlaySound(buttonSound, transform.position);

        if (isInElevator)
        {
            if(playerStats.GetHasFoundButton())
            {
                Debug.Log("Button pressed, elevator is going down");
                playerStats.SetHasFoundButton(false);
                playerStats.NextFloor();
            }
            else
            {
                Debug.Log("You need to find the button first");
            }
        }
        else
        {
            playerStats.SetHasFoundButton(true);
            buttonFound = true;
        }
    }
}
