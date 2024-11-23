using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaPlayerUI : MonoBehaviour
{
    [SerializeField] GameObject invSquarePrefab;

    [SerializeField] HorizontalLayoutGroup invPanel;
    [SerializeField] Slider sliderBattery;

    List<GameObject> squares = new List<GameObject>();


    [Tooltip("Sprites for the UI, name must be the same as the enum.")]
    [SerializeField] List<Sprite> sprites = new List<Sprite>();



    RechargableItem rechargableItem;

    Inventory inventory;
    PlayerStats playerStats;


    private void Start()
    {
        inventory = Inventory.Instance();
        playerStats = PlayerStats.Instance();
        rechargableItem = null;
        sliderBattery.gameObject.SetActive(false);
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
                ItemSpawner.ItemType itemType = itemToPickup.GetItemType();
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

            sliderBattery.GetComponent<SliderBattery>().UpdateBattery(charge);
            sliderBattery.value = charge;
        }
        else
        {
            sliderBattery.gameObject.SetActive(false);
        }
    }


}
