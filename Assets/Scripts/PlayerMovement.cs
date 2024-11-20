using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] CharacterController characterController;

    [SerializeField] InputActionReference movement;


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
    }

    private void OnDisable()
    {
        movement.action.Disable();
    }





    void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

        move = transform.TransformDirection(move);

        characterController.SimpleMove(move * speed);
    }
}
