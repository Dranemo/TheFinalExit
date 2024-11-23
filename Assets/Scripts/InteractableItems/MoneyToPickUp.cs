using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoneyToPickUp : Interactable
{
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
        Destroy(transform.parent.gameObject);
    }
}
