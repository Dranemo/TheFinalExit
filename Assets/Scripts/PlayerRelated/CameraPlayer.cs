using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameras;


    [SerializeField] private float mouseSensitivity = 500f;
    [SerializeField] InputActionReference look;

    private float xRotationCamera = 0f;

    private Vector2 rotation;

    float rotationX;
    float rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }




    private void OnEnable()
    {
        look.action.Enable();
    }

    private void OnDisable()
    {
        look.action.Disable();
    }



    void Update()
    {

        rotation = look.action.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;



        xRotationCamera -= rotation.y;
        xRotationCamera = Mathf.Clamp(xRotationCamera, -90f, 90f);

        foreach (var cam in cameras) 
            cam.transform.localRotation = Quaternion.Euler(xRotationCamera, 0f, 0f);

        transform.Rotate(Vector3.up * rotation.x);
    }
}