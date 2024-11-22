using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] CanvaPlayerUI canvaPlayerUI;
    static public Inventory instance;


    [SerializeField] InputActionReference scrollMouse;


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
    }

    private void OnEnable()
    {
        scrollMouse.action.Enable();
    }

    private void OnDisable()
    {
        scrollMouse.action.Disable();
    }

    void Update()
    {


        float scrollValue = scrollMouse.action.ReadValue<float>() / 120;
        Debug.Log(scrollValue);

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

            SetItemInHand(index);
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








    private void SetItemInHand(GameObject _object)
    {
        if(itemInHand != null)
            itemInHand.gameObject.SetActive(false);

        itemInHand = _object.GetComponent<ItemUsable>();
        itemInHand.gameObject.SetActive(true);

        canvaPlayerUI.UpdateSelectedItem(items.IndexOf(itemInHand));
    }
    private void SetItemInHand(int index)
    {
        if(index < 0 || index >= items.Count)
        {
            return;
        }

        if(itemInHand != null)
            itemInHand.gameObject.SetActive(false);

        itemInHand = items[index];
        itemInHand.gameObject.SetActive(true);

        canvaPlayerUI.UpdateSelectedItem(items.IndexOf(itemInHand));
    }





    public List<ItemUsable> GetInventory()
    {
        return items;
    }
}
