using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingMonster : MonoBehaviour
{
    Camera cameraPlayer;
    PlayerStats playerStats;
    [SerializeField] float angle = 30;

    void Start()
    {
        cameraPlayer = Camera.main;
        playerStats = PlayerStats.Instance();
    }


    private void Update()
    {
        Vector3 direction = cameraPlayer.transform.position - transform.position;
        Vector3 cameraLocalForward = cameraPlayer.transform.forward;

        float angleBetween = Vector3.Angle(direction, -cameraLocalForward);
        if (angleBetween < angle)
        {
            PlayerStats.Instance().TakeDamage(5);
        }
    }
}
