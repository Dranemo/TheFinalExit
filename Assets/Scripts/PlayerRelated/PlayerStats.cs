using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    int health = 100;
    int maxHealth = 100;

    int money = 0;

    static PlayerStats instance;
    public static PlayerStats Instance()
    {
        if (instance == null)
        {
            instance = new PlayerStats();
        }
        return instance;
    }



    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }
    public void RemoveMoney(int amount) {
        money -= amount;
    }
}
