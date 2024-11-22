using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] Outline outline;
    [SerializeField] List<GameObject> buttonDisplay;

    [SerializeField] InputActionReference interact;

    protected bool isOutlined = false;

    public void SetIsOutlined(bool _isOutlined)
    {
        if (_isOutlined == isOutlined)
        {
            return;
        }


        isOutlined = _isOutlined;
        outline.enabled = _isOutlined;

        if (buttonDisplay != null)
        {
            foreach (var button in buttonDisplay)
            {
                button.SetActive(_isOutlined);
            }
        }
    }


    private void OnEnable()
    {
        interact.action.Enable();
        interact.action.performed += Interact;


        outline.enabled = false;

        if (buttonDisplay != null)
        {
            foreach (var button in buttonDisplay)
            {
                button.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        interact.action.performed -= Interact;


        outline.enabled = false;

        if (buttonDisplay != null)
        {
            foreach (var button in buttonDisplay)
            {
                button.SetActive(false);
            }
        }
    }






    public virtual void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Oh, tu as assigné le script Interactable au lieu de la précision de l'objet, t'abuses");
    }
}
