using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] Outline outline;
    [SerializeField] GameObject buttonDisplay;

    [SerializeField] InputActionReference interact;

    protected bool isOutlined = false;

    public void SetIsOutlined(bool _isOutlined)
    {
        if (_isOutlined == isOutlined)
        {
            return;
        }


        isOutlined = _isOutlined;
        outline.OutlineWidth = (_isOutlined ? 6 : 0);

        if(buttonDisplay != null)
        buttonDisplay.SetActive(_isOutlined);
    }


    private void OnEnable()
    {
        interact.action.Enable();
        interact.action.performed += Interact;
    }

    private void OnDisable()
    {
        interact.action.Disable();
        interact.action.performed -= Interact;
    }




    private void Start()
    {
        outline.OutlineWidth = 0;

        if(buttonDisplay != null)
        buttonDisplay.SetActive(false);
    }




    public virtual void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Oh, tu as assigné le script Interactable au lieu de la précision de l'objet, t'abuses");
    }
}
