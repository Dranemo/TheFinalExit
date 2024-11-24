using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HidingPlace : Interactable
{
    [SerializeField] List<GameObject> virtualCams;
    [SerializeField] VolumeProfile volumeProfile;
    [SerializeField] AudioClip ScreamerHiding;

    private Vignette vignette;

    GameObject closestVirtualCam;



    Coroutine intensityCoroutine;
    bool coroutineRunningUp = false;
    bool coroutineRunningDown = false;


    GameObject player;
    bool isHidden = false;


    private void Start()
    {
        foreach (GameObject cam in virtualCams)
        {
            cam.SetActive(false);
        }
        player = PlayerSingleton.GetPlayer();

        // Récupérer le composant Vignette du VolumeProfile
        if (volumeProfile.TryGet(out Vignette v))
        {
            vignette = v;
        }
        else
        {
            Debug.LogError("Vignette n'est pas trouvé dans le VolumeProfile.");
        }

        intensityCoroutine = null;

        vignette.intensity.value = 0.3f;

    }

    private void Update()
    {
        if (isHidden && !coroutineRunningUp)
        {
            coroutineRunningUp = true;
            coroutineRunningDown = false;

            if (intensityCoroutine != null)
                StopCoroutine(intensityCoroutine);
            intensityCoroutine = StartCoroutine(ChangeVignetteIntensity(vignette.intensity.value, 1f, 15f));
        }
    }


    public override void Interact(InputAction.CallbackContext context)
    {
        if(isOutlined && !isHidden)
        {
            base.Interact(context);
            isHidden = true;
            player.SetActive(false);
            Camera.main.GetComponent<RaycastingInteraction>().enabled = false;

            ItemUsable itemInHand = Inventory.Instance().GetItemInHand();
            if (itemInHand != null)
                itemInHand.gameObject.SetActive(false);

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
            AudioManager.Instance.SetHeartBeatSound(true, Camera.main.transform.position);

            Debug.Log("Interacting with hiding place");
        }
        else if(isHidden && !coroutineRunningDown)
        {
            base.Interact(context);
            AudioManager.Instance.SetHeartBeatSound(false, Vector3.zero);

            coroutineRunningDown = true;
            coroutineRunningUp = false;

            if (intensityCoroutine != null)
                StopCoroutine(intensityCoroutine);
            intensityCoroutine = StartCoroutine(ChangeVignetteIntensity(vignette.intensity.value, 0.3f, 1f));

            isHidden = false;
            PlayerSingleton.GetPlayer().SetActive(true);

            ItemUsable itemInHand = Inventory.Instance().GetItemInHand();
            if (itemInHand != null)
                itemInHand.gameObject.SetActive(true);

            closestVirtualCam.SetActive(!closestVirtualCam.activeSelf);
            Camera.main.GetComponent<RaycastingInteraction>().enabled = true;
            Debug.Log("Interacting with hiding place");
        }
    }


    IEnumerator ChangeVignetteIntensity(float start, float end, float duration)
    {
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(start, end, t / duration);

            yield return null;
        }

        if(start < end)
        {
            CanvaPlayerUI.Instance().SetScreenBlack();
            AudioManager.Instance.SetHeartBeatSound(false, Vector3.zero);
            AudioManager.Instance.PlaySound(ScreamerHiding, Camera.main.transform.position);
            PlayerStats.Instance().TakeDamage(100);
        }

        intensityCoroutine = null;
        coroutineRunningUp = false;
        coroutineRunningDown = false;
    }
}
