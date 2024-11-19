using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;


    Vector3 movementX;
    Vector3 movementZ;


    [SerializeField] float speed = 10f;
    [SerializeField] float horizontalInput = 0f;
    [SerializeField] float verticalInput = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Mouvement horizontal
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        movementZ = transform.TransformDirection(Vector3.forward) * verticalInput * speed;
        movementX = transform.TransformDirection(Vector3.right) * horizontalInput * speed;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(movementX);
        GetComponent<Rigidbody>().AddForce(movementZ);
    }
}
