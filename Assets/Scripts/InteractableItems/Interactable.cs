using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] List<Outline> outlines;
    [SerializeField] List<GameObject> buttonDisplay;

    [SerializeField] protected InputActionReference interact;

    protected bool isOutlined = false;

    public void SetIsOutlined(bool _isOutlined)
    {
        if (_isOutlined == isOutlined)
        {
            return;
        }


        isOutlined = _isOutlined;

        foreach (var outline in outlines)
        {
            outline.enabled = _isOutlined;
        }

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

        foreach (var outline in outlines)
        {
            outline.enabled = false;
        }

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

        foreach (var outline in outlines)
        {
            outline.enabled = false;
        }

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
        OnActionPerformed(context);

        Debug.Log("Oh, tu as assigné le script Interactable au lieu de la précision de l'objet, t'abuses");
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        PlayerStats.SetDevice(context.control.device);
    }
}
