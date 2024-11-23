using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] CharacterController characterController;
    [SerializeField] GameObject crouchCam;

    [SerializeField] InputActionReference movement;
    [SerializeField] InputActionReference crouch;

    bool crouching = false;


    Vector2 movementInput = Vector2.zero;
    [SerializeField] float speed = 10f;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        movement.action.Enable();
        crouch.action.Enable();
        crouch.action.performed += Crouch;
    }

    private void OnDisable()
    {
        movement.action.Disable();
        crouch.action.Enable();
        crouch.action.performed += Crouch;
    }





    void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

        move = transform.TransformDirection(move);

        characterController.SimpleMove(move * speed);
    }

    void Crouch(InputAction.CallbackContext context)
    {
        crouching = !crouching;
        crouchCam.SetActive(crouching);
    }
}
