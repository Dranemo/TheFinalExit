using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareInvUI : MonoBehaviour
{
    [SerializeField] Image itemSprite;

    [SerializeField] Sprite[] SquaresCases;



    public void SetItemSprite(Sprite sprite)
    {
        itemSprite.sprite = sprite;
    }

    public void SetSelected(bool _bool)
    {
        if (_bool)
        {
            GetComponent<Image>().sprite = SquaresCases[1];
        }
        else
        {
            GetComponent<Image>().sprite = SquaresCases[0];
        }
    }
}
