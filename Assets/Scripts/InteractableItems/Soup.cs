using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Soup : Interactable
{
    PlayerStats playerStats;
    [SerializeField] int healthAmount = 25;
    [SerializeField] AudioClip cronch;

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

        if (playerStats.GetHealth() < playerStats.GetMaxHealth())
        {
            AudioManager.Instance.PlaySound(cronch, transform.position);
            playerStats.Heal(healthAmount);
            Destroy(transform.parent.gameObject);
        }
    }
}
