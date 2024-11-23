using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastingInteraction : MonoBehaviour
{
    [SerializeField] float rayLength = 3f; // Longueur du rayon
    [SerializeField] LayerMask layerMask; // Masque de collision
    private Interactable lastHitInteractable;

    void FixedUpdate()
    {
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, rayDirection, out hit, rayLength, layerMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                if (lastHitInteractable != null && lastHitInteractable != interactable)
                {
                    lastHitInteractable.SetIsOutlined(false);
                }

                interactable.SetIsOutlined(true);
                lastHitInteractable = interactable;
            }
            else
            {
                if (lastHitInteractable != null)
                {
                    lastHitInteractable.SetIsOutlined(false);
                    lastHitInteractable = null;
                }
            }
        }
        else
        {
            if (lastHitInteractable != null)
            {
                lastHitInteractable.SetIsOutlined(false);
                lastHitInteractable = null;
            }
        }
    }

}
