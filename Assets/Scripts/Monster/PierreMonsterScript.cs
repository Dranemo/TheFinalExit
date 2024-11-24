using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierreMonsterScript : MonoBehaviour
{
    Camera camera;
    PlayerStats playerStats;
    [SerializeField] Transform image;
    [SerializeField] LayerMask layerMask;

    [SerializeField] AudioClip scream;
    bool damageTaken = false;


    void Start()
    {
        camera = Camera.main;
        playerStats = PlayerStats.Instance();
    }

    private void FixedUpdate()
    {
        if (playerStats.GetPlayerHidden())
        {
            return;
        }
        if(damageTaken)
        {
            return;
        }

        damageTaken = true;

        Vector3 direction = image.position - camera.transform.position;
        float distance = direction.magnitude;

        // Effectuer un raycast pour vérifier les obstacles
        if (!Physics.Raycast(camera.transform.position, direction, out RaycastHit hit, distance, layerMask))
        {
            CanvaPlayerUI.Instance().ShowScreamer();
            AudioManager.Instance.PlaySound(scream, image.position);
            playerStats.TakeDamage(100);
        }
    }

}
