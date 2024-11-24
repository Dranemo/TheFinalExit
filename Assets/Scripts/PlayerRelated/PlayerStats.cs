using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    int health = 100;
    int maxHealth = 100;

    int money = 0;

    static PlayerStats instance;
    Coroutine invicibilityFramesCoroutine;


    public static PlayerStats Instance()
    {
        if (instance == null)
        {
            instance = new PlayerStats();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }



    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        CanvaPlayerUI.Instance().UpdateHealth();
    }

    public void TakeDamage(int amount)
    {
        if (invicibilityFramesCoroutine == null)
        {
            invicibilityFramesCoroutine = StartCoroutine(InvicibilityFrames(amount, 0.5f));
        }
        
    }
    public void AddMoney(int amount)
    {
        money += amount;
        CanvaPlayerUI.Instance().UpdateMoney();
    }
    public void RemoveMoney(int amount) {
        money -= amount;
        CanvaPlayerUI.Instance().UpdateMoney();
    }



    public int GetHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetMoney()
    {
        return money;
    }




    IEnumerator InvicibilityFrames(int amount, float duration)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
        CanvaPlayerUI.Instance().UpdateHealth();


        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        invicibilityFramesCoroutine = null;
    }
}
