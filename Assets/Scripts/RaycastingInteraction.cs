using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastingInteraction : MonoBehaviour
{
    [SerializeField] float rayLength = 3f; // Longueur du rayon
    [SerializeField] LayerMask layerMask; // Masque de collision
    private Door lastHitDoor;

    void Update()
    {
        Vector3 rayDirection = transform.forward.normalized;
        Vector3 rayEnd = transform.position + rayDirection * rayLength;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDirection, out hit, rayLength, layerMask))
        {
            if (hit.collider != null)
            {
                Door door = hit.collider.GetComponent<Door>();

                if (door != null)
                {
                    if (lastHitDoor != null && lastHitDoor != door)
                    {
                        lastHitDoor.SetIsOutlined(false);
                    }

                    door.SetIsOutlined(true);
                    lastHitDoor = door;
                }

                else
                {

                    if (lastHitDoor != null)
                    {
                        lastHitDoor.SetIsOutlined(false);
                        lastHitDoor = null;
                    }
                }
            }
        }
        else
        {
            if (lastHitDoor != null)
            {
                lastHitDoor.SetIsOutlined(false);
                lastHitDoor = null;
            }
        }
    }

}
