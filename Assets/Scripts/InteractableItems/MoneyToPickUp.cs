using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoneyToPickUp : Interactable
{
    [SerializeField] AudioClip takeSound;

    [SerializeField] int Value = 0;
    PlayerStats playerStats;

    void Start()
    {
        playerStats = PlayerStats.Instance();
    }

    public override void Interact(InputAction.CallbackContext context)
    {
        if(!isOutlined)
        {
            return;
        }

        playerStats.AddMoney(Value);
        AudioManager.Instance.PlaySound(takeSound, transform.position);
        Destroy(transform.parent.gameObject);
    }
}
