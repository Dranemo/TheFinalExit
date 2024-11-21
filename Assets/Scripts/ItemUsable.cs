using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemUsable : MonoBehaviour
{

    [SerializeField] InputActionReference use;
    public Inventory.ItemType itemType;




    private void OnEnable()
    {
        use.action.Enable();
        use.action.performed += Use;
    }

    private void OnDisable()
    {
        use.action.Disable();
        use.action.performed -= Use;
    }



    public virtual void Use(InputAction.CallbackContext context)
    {
        Debug.Log("Using " + name);
    }
}
