using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBattery : MonoBehaviour
{
    Color green = new Color(0, 1, 0, 1);
    Color red = new Color(1, 0, 0, 1);
    Color yellow = new Color(1, 1, 0, 1);

    [SerializeField] GameObject fillThingy;
    Image fillImage;

    Coroutine colorChange;

    private void Start()
    {
        fillImage = fillThingy.GetComponent<UnityEngine.UI.Image>();
    }

    public void UpdateBattery(float charge)
    {
        if (charge > 75 && colorChange == null && fillImage.color != green)
        {
            colorChange = StartCoroutine(SmoothColorChange(fillImage.color, green, 0.5f));
        }
        else if (charge > 25 && charge <= 75 && colorChange == null && fillImage.color != yellow)
        {
            colorChange = StartCoroutine(SmoothColorChange(fillImage.color, yellow, 0.5f));
        }
        else if (charge <= 25 && colorChange == null && fillImage.color != red)
        {
            colorChange = StartCoroutine(SmoothColorChange(fillImage.color, red, 0.5f));
        }
    }

    IEnumerator SmoothColorChange(Color start, Color end, float duration)
    {
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            fillImage.color = Color.Lerp(start, end, t / duration);

            yield return null;
        }

        colorChange = null;
    }
}
