using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] CanvaPlayerUI canvaPlayerUI;
    List<ItemUsable> items;
    ItemUsable itemInHand = null;

    [Tooltip ("ItemPrefabs")]
    [SerializeField] GameObject[] itemPrefabs;

    Dictionary<string, GameObject> itemPrefabsDic;

    Transform mainCameraTransform;



     public enum ItemType { 
        Torch,
    };


    int size = 5;


    public List<ItemUsable> GetInventory()
    {
        return items;
    }


    private void Start()
    {
        items = new List<ItemUsable>();
        mainCameraTransform = Camera.main.transform;

        foreach (GameObject item in itemPrefabs)
        {
            itemPrefabsDic.Add(item.GetComponent<ItemUsable>().itemType.ToString(), item);
        }
    }

    public bool AddItem(ItemType itemType)
    {
        if (items.Count < size)
        {
            GameObject item = Instantiate(itemPrefabsDic[itemType.ToString()], mainCameraTransform);
            item.transform.SetParent(mainCameraTransform, true);

            item.GetComponent<ItemUsable>().enabled = true;
            item.GetComponent<ItemToPickup>().enabled = false;
            item.GetComponent<Rigidbody>().isKinematic = true;

            items.Add(item.GetComponent<ItemUsable>());

            canvaPlayerUI.UpdateSquares();

            return true;
        }
        return false;
    }
    public bool AddItem(GameObject itemFound)
    {
        if (items.Count < size)
        {
            itemFound.transform.SetParent(mainCameraTransform, true);
            itemFound.transform.localPosition = Vector3.zero;

            itemFound.GetComponent<ItemUsable>().enabled = true;
            itemFound.GetComponent<ItemToPickup>().enabled = false;
            itemFound.GetComponent<Rigidbody>().isKinematic = true;

            items.Add(itemFound.GetComponent<ItemUsable>());

            canvaPlayerUI.UpdateSquares();

            return true;
        }
        return false;
    }






    public void DropItem() {
        if(itemInHand != null)
        {
            items.Remove(itemInHand);
            itemInHand.transform.parent = null;

            itemInHand.GetComponent<ItemUsable>().enabled = false;
            itemInHand.GetComponent<ItemToPickup>().enabled = true;
            itemInHand.GetComponent<Rigidbody>().isKinematic = false;

            canvaPlayerUI.UpdateSquares();
        }
    }






    public void SetItemInHand(ItemUsable _item)
    {
        if(itemInHand != null)
        {
            itemInHand.gameObject.SetActive(false);
        }


        if (_item != itemInHand)
        {
            itemInHand = _item;
            itemInHand.gameObject.SetActive(true);
        }
        else
        {
            itemInHand = null;
            itemInHand.gameObject.SetActive(false);
        }
    }
    public void SetItemInHand(int index)
    {
        if (itemInHand != null)
        {
            itemInHand.gameObject.SetActive(false);
        }


        if (index < items.Count)
        {
            if (items[index] != itemInHand)
            {
                itemInHand = items[index];
                itemInHand.gameObject.SetActive(true);
            }
            else
            {
                itemInHand = null;
                itemInHand.gameObject.SetActive(false);
            }
        }
    }
}
