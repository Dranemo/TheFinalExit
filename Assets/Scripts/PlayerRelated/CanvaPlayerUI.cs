using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CanvaPlayerUI : MonoBehaviour
{
    static CanvaPlayerUI instance;


    [SerializeField] GameObject invSquarePrefab;

    [SerializeField] HorizontalLayoutGroup invPanel;
    [SerializeField] Slider sliderBattery;
    [SerializeField] Slider sliderHealth;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] Image bloodDamageImage;
    [SerializeField] Image blackScreen;
    [SerializeField] Image ScreamerPierre;

    List<GameObject> squares = new List<GameObject>();


    [Tooltip("Sprites for the UI, name must be the same as the enum.")]
    [SerializeField] List<Sprite> sprites = new List<Sprite>();



    RechargableItem rechargableItem;

    Inventory inventory;
    PlayerStats playerStats;

    Coroutine healthCoroutine = null;

    Color redBlood = new Color(1, 0, 0, 0.5f);
    Color clearBlood = new Color(1, 0, 0, 0);





    private void Awake()
    {
        instance = this;
    }
    static public CanvaPlayerUI Instance()
    {
        if(instance == null)
        {
            instance = new CanvaPlayerUI();
        }
        return instance;
    }





    public void SetScreenBlack()
    {
        blackScreen.enabled = true;
    }
    public void ShowScreamer()
    {
        ScreamerPierre.enabled = true;
    }




    private void Start()
    {
        inventory = Inventory.Instance();
        playerStats = PlayerStats.Instance();
        rechargableItem = null;
        sliderBattery.gameObject.SetActive(false);

        sliderHealth.maxValue = playerStats.GetMaxHealth();
        sliderHealth.value = playerStats.GetHealth();
    }


    private void Update()
    {
        if(rechargableItem != null && rechargableItem.GetIsOn())
            UpdateBattery();
    }



    public void UpdateSquares()
    {
        foreach (var square in squares)
        {
            Destroy(square);
        }
        squares.Clear();

        for (int i = 0; i < inventory.GetInventory().Count; i++)
        {
            GameObject square = Instantiate(invSquarePrefab, invPanel.transform);

            ItemToPickup itemToPickup = inventory.GetInventory()[i].gameObject.GetComponent<ItemToPickup>();
            if (itemToPickup == null)
            {
                itemToPickup = inventory.GetInventory()[i].transform.GetComponentInChildren<ItemToPickup>();
            }

            if (itemToPickup != null)
            {
                ItemSpawner.UsableItemType itemType = itemToPickup.GetItemType();
                square.GetComponent<SquareInvUI>().SetItemSprite(sprites[(int)itemType]);
            }


            squares.Add(square);
        }
    }
    public void UpdateSelectedItem(int index)
    {
        bool rechargeItemChanged = false;

        for (int i = 0; i < squares.Count; i++)
        {
            if (i == index)
            {
                squares[i].GetComponent<SquareInvUI>().SetSelected(true);

                RechargableItem temp = inventory.GetItemInHand().GetComponent<RechargableItem>();
                if(temp == null)
                {
                    temp = inventory.GetItemInHand().transform.GetComponentInChildren<RechargableItem>();
                }
                rechargableItem = temp;
                rechargeItemChanged = true;
            }
            else
            {
                squares[i].GetComponent<SquareInvUI>().SetSelected(false);
            }
        }


        if(!rechargeItemChanged && rechargableItem != null)
        {
            rechargableItem = null;
        }

        UpdateBattery();
    }




    private void UpdateBattery()
    {
        ItemUsable itemInHand = inventory.GetItemInHand();
        if(rechargableItem != null)
        {
            sliderBattery.gameObject.SetActive(true);
            float charge = rechargableItem.GetCharge();

            sliderBattery.GetComponent<SliderBattery>().UpdateBattery(charge, true);
            sliderBattery.value = charge;
        }
        else
        {
            sliderBattery.gameObject.SetActive(false);
        }
    }


    public void UpdateHealth()
    {
        if (healthCoroutine != null)
            StopCoroutine(healthCoroutine);

        healthCoroutine = StartCoroutine(UpdateHealthSmooth(sliderHealth.value, playerStats.GetHealth(), 1f));
    }
    public void UpdateMoney()
    {
        coinText.text = playerStats.GetMoney().ToString();
    }




    IEnumerator UpdateHealthSmooth(float start, float end, float duration)
    {
        float t = 0;
        if (start > end)
        {
            bloodDamageImage.color = redBlood;
        }
        Color startColor = bloodDamageImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            sliderHealth.value = Mathf.Lerp(start, end, t / duration);
            bloodDamageImage.color = Color.Lerp(startColor, clearBlood, t / duration);



            yield return null;
        }
        sliderHealth.value = end;
        bloodDamageImage.color = clearBlood;

        healthCoroutine = null;
    }
}
