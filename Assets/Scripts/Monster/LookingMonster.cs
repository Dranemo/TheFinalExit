using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookingMonster : MonoBehaviour
{
    Camera cameraPlayer;
    PlayerStats playerStats;
    [SerializeField] float angle = 30;
    [SerializeField] float angleStartGlitching = 45;

    [SerializeField] Transform face;
    [SerializeField] Image glitchScreen;
    [SerializeField] LayerMask layerMask;

    void Start()
    {
        cameraPlayer = Camera.main;
        playerStats = PlayerStats.Instance();
    }


    private void FixedUpdate()
    {
        Vector3 direction = cameraPlayer.transform.position - face.position;
        Vector3 cameraLocalForward = cameraPlayer.transform.forward;

        

        float angleBetween = Vector3.Angle(direction, -cameraLocalForward);
        if (angleBetween < angleStartGlitching)
        {
            // Effectuer un raycast pour v�rifier les obstacles
            Debug.DrawLine(face.position, face.position + direction, Color.red);
            if (!Physics.Raycast(face.position, direction, out RaycastHit hit, direction.magnitude, layerMask))
            {
                // Calculer l'alpha en fonction de l'angle
                float alpha = Mathf.Lerp(1, 0, angleBetween / angleStartGlitching);

                // Appliquer l'alpha � l'image
                Color color = glitchScreen.color;
                color.a = alpha;
                glitchScreen.color = color;

                if (angleBetween < angle)
                {
                    PlayerStats.Instance().TakeDamage(5);
                }
            }
            else
            {
                // R�initialiser l'alpha � 0 si un obstacle est d�tect�
                Color color = glitchScreen.color;
                color.a = 0;
                glitchScreen.color = color;
            }
        }
        else
        {
            // R�initialiser l'alpha � 0 si l'angle est sup�rieur ou �gal � 90
            Color color = glitchScreen.color;
            color.a = 0;
            glitchScreen.color = color;
        }
    }
}
