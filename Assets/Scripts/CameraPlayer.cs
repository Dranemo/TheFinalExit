using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 500f;

    [SerializeField] private Transform playerBody;

    private float xRotationCamera = 0f;

    float rotationX;
    float rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        rotationX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;




        xRotationCamera -= rotationY;
        xRotationCamera = Mathf.Clamp(xRotationCamera, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotationCamera, 0f, 0f);
        playerBody.Rotate(Vector3.up * rotationX);
    }

}