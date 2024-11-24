using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    CanvaPlayerUI canvaPlayerUI;
    static public Inventory instance;



    [SerializeField] InputActionReference scrollMouse;
    [SerializeField] InputActionReference drop;
    [SerializeField] List<InputActionReference> keysChangeItem;


    List<ItemUsable> items = new();
    int size = 5;
    ItemUsable itemInHand;

    Transform mainCameraTransform;

    private void Awake()
    {

        instance = this;
    }
    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        size = keysChangeItem.Count;
        canvaPlayerUI = CanvaPlayerUI.Instance();
    }

    private void OnEnable()
    {
        scrollMouse.action.Enable();
        drop.action.Enable();
        drop.action.performed += DropItem;

        for(int i = 0; i < keysChangeItem.Count; i++)
        {
            int index = i;
            keysChangeItem[i].action.Enable();
            keysChangeItem[i].action.performed += (context) => SetItemInHand(index);
        }
    }

    private void OnDisable()
    {
        scrollMouse.action.Disable();
        drop.action.Disable();
        drop.action.performed -= DropItem;

        for (int i = 0; i < keysChangeItem.Count; i++)
        {
            int index = i;
            keysChangeItem[i].action.Disable();
            keysChangeItem[i].action.performed -= (context) => SetItemInHand(index);
        }
    }

    void Update()
    {


        float scrollValue = scrollMouse.action.ReadValue<float>() / 120;

        if (scrollValue != 0)
        {
            int index = items.IndexOf(itemInHand);
            index += (int)scrollValue;
            if (index < 0)
            {
                index = items.Count - 1;
            }
            else if (index >= items.Count)
            {
                index = 0;
            }

            SetItemInHand(index, true);
        }
    }







    static public Inventory Instance()
    {
        if (instance == null)
        {
            instance = new Inventory();
        }
        return instance;
    }
    public void AddItem(GameObject _object)
    {
        if(items.Count >= size)
        {
            return;
        }

        ItemUsable item = _object.GetComponent<ItemUsable>();
        if(item != null)
        {
            _object.transform.GetComponentInChildren<MeshCollider>().enabled = false;
            _object.GetComponent<Rigidbody>().isKinematic = true;

            _object.transform.GetComponentInChildren<ItemToPickup>().enabled = false;
            item.enabled = true;

            _object.transform.SetParent(mainCameraTransform);

            _object.transform.localPosition = new Vector3(0.4f, -0.3f, 1);
            _object.transform.localRotation = Quaternion.Euler(0, 90, 0);



            items.Add(item);
            canvaPlayerUI.UpdateSquares();
            SetItemInHand(_object);
        }
    }








    private void SetItemInHand(GameObject _object, bool dropped = false)
    {
        if(_object == null)
        {
            if (itemInHand != null && !dropped)
                itemInHand.gameObject.SetActive(false);

            itemInHand = null;
            canvaPlayerUI.UpdateSelectedItem(-1);
            return;
        }

        if (itemInHand != null)
            itemInHand.gameObject.SetActive(false);

        itemInHand = _object.GetComponent<ItemUsable>();
        itemInHand.gameObject.SetActive(true);

        canvaPlayerUI.UpdateSelectedItem(items.IndexOf(itemInHand));
    }
    private void SetItemInHand(int index, bool scrolled = false)
    {
        if(index < 0 || index >= items.Count)
        {
            return;
        }

        int indexOld = items.IndexOf(itemInHand);
        if (itemInHand != null)
        {
            if(scrolled && items.Count != 1 || !scrolled)
            {
                itemInHand.gameObject.SetActive(false);
                itemInHand = null;
            }
        }


        if (index != indexOld || scrolled)
        {
            itemInHand = items[index];
            itemInHand.gameObject.SetActive(true);
        }

        canvaPlayerUI.UpdateSelectedItem(items.IndexOf(itemInHand));
    }




    private void DropItem(InputAction.CallbackContext context)
    {
        if(itemInHand != null)
        {
            itemInHand.transform.SetParent(null);
            itemInHand.transform.GetComponentInChildren<MeshCollider>().enabled = true;
            itemInHand.GetComponent<Rigidbody>().isKinematic = false;

            itemInHand.transform.GetComponentInChildren<ItemToPickup>().enabled = true;
            itemInHand.enabled = false;


            items.Remove(itemInHand);
            canvaPlayerUI.UpdateSquares();
            SetItemInHand(null, true);
        }
    }



    public List<ItemUsable> GetInventory()
    {
        return items;
    }
    public ItemUsable GetItemInHand()
    {
        return itemInHand;
    }
}
