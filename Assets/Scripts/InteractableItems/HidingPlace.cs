using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingPlace : Interactable
{
    [SerializeField] List<GameObject> virtualCams;
    GameObject closestVirtualCam;

    GameObject player;
    bool isHidden = false;


    private void Start()
    {
        foreach (GameObject cam in virtualCams)
        {
            cam.SetActive(false);
        }
        player = PlayerSingleton.GetPlayer();

    }


    public override void Interact(InputAction.CallbackContext context)
    {
        if(isOutlined && !isHidden)
        {
            isHidden = true;
            player.SetActive(false);

            float closesDist = Mathf.Infinity;
            foreach (GameObject cam in virtualCams)
            {
                if(Vector3.Distance(cam.transform.position, player.transform.position) < closesDist)
                {
                    closesDist = Vector3.Distance(cam.transform.position, player.transform.position);
                    closestVirtualCam = cam;
                }
            }

            closestVirtualCam.SetActive(!closestVirtualCam.activeSelf);
            Debug.Log("Interacting with hiding place");
        }
        else if(isHidden)
        {
            isHidden = false;
            PlayerSingleton.GetPlayer().SetActive(true);

            closestVirtualCam.SetActive(!closestVirtualCam.activeSelf);
            Debug.Log("Interacting with hiding place");
        }
    }
}
