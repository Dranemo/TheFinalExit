using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightsOffGestion : MonoBehaviour
{
    [SerializeField] GameObject[] lights;
    [SerializeField] AudioClip[] shatteringSounds;
    [SerializeField] AudioClip flashingLight;
    public bool lightsOff = false;

    private void Start()
    {
        int random = Random.Range(0, 4);

        if (random == 0)
        {
            lightsOff = true;
            foreach (GameObject light in lights)
            {
                foreach (Transform child in light.transform)
                {
                    StartCoroutine(TurnOffLight(child.gameObject));
                }
            }
        }
    }

    public GameObject[] GetLights()
    {
        return lights;
    }

    public void TurnOffLights()
    {
        foreach (GameObject light in lights)
        {
            foreach (Transform child in light.transform)
            {
                StartCoroutine(TurnOffLight(child.gameObject));
            }
        }
    }
    private IEnumerator TurnOffLight(GameObject light)
    {
        int random = Random.Range(0, 10);
        float t = 0;

        while (t < random/10)
        {
            t += Time.deltaTime;
            yield return null;
        }

        light.SetActive(false);
        random = Random.Range(0, 1);
        AudioManager.Instance.PlaySound(shatteringSounds[random], light.transform.position);
    }
    
    public void FlashingLights(float duration)
    {
        Debug.Log("FlashingLights");

        foreach (GameObject light in lights)
        {
            StartCoroutine(FlashingLight(light, duration));
        }
    }
    private IEnumerator FlashingLight(GameObject light, float duration)
    {
        float t1 = 0;
        AudioManager.Instance.PlaySound(flashingLight, light.transform.position);

        while (t1 < duration)
        {
            t1 += Time.deltaTime;
            yield return null;


            int random2 = Random.Range(1, 7);
            float t = 0;

            while (t < random2 / 10)
            {
                t += Time.deltaTime;
                yield return null;
            }
            light.SetActive(!light.activeSelf);

        }
    }

}
