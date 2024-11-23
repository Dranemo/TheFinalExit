using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Tooltip ("List of Prefabs. Each prefab should have a script that inherits from ItemUsable. Prefab Name must be the same as the enum name in ItemType.")]
    [SerializeField] List<GameObject> itemsPrefabs;


    static Dictionary<string, GameObject> itemsPrefabsDict = new Dictionary<string, GameObject>();
    public enum ItemType
    {
        Torch,
        CoinPile,
        CoinPile2,
        CoinPile3,
    };


    private void Awake()
    {
        foreach (GameObject item in itemsPrefabs)
        {
            itemsPrefabsDict.Add(item.name, item);
        }
    }




    static public GameObject SpawnObject(ItemType itemType, Vector3 SpawningPosition, Quaternion rotation, GameObject parent)
    {
        GameObject spawnedItem = Instantiate(itemsPrefabsDict[itemType.ToString()], SpawningPosition, rotation);
        spawnedItem.transform.parent = parent.transform;




        ItemUsable itemUsable = spawnedItem.GetComponent<ItemUsable>();
        if(itemUsable != null)
        {
            itemUsable.enabled = false;
        }
        Rigidbody rb = spawnedItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }




        return spawnedItem;
    }
}
