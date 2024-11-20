using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastingInteraction : MonoBehaviour
{
    [SerializeField] float rayLength = 3f; // Longueur du rayon
    [SerializeField] LayerMask layerMask; // Masque de collision
    private Door lastHitDoor;

    void FixedUpdate()
    {
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, rayDirection, out hit, rayLength, layerMask))
        {
            Door door = hit.collider.GetComponent<Door>();

            if (door != null)
            {
                if (lastHitDoor != null && lastHitDoor != door)
                {
                    Debug.Log("ni là");
                    lastHitDoor.SetIsOutlined(false);
                }

                Debug.Log("Door hit");
                door.SetIsOutlined(true);
                lastHitDoor = door;
            }
            else
            {

                Debug.Log("devrait pas apsser là");
                if (lastHitDoor != null)
                {
                    lastHitDoor.SetIsOutlined(false);
                    lastHitDoor = null;
                }
            }
        }
        else
        {
            if (lastHitDoor != null)
            {
                Debug.Log("No hit");

                lastHitDoor.SetIsOutlined(false);
                lastHitDoor = null;
            }
        }
    }

}
