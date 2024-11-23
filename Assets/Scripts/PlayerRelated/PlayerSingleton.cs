using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    static GameObject player;

    public static GameObject GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        return player;
    }

    private void Awake()
    {
        player = this.gameObject;
    }
}
