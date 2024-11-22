using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningItemScript : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPoints;

    private void Start()
    {
        SpawnItem();
    }


    public void SpawnItem()
    {
        ItemSpawner.SpawnObject(ItemSpawner.ItemType.Torch, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.identity, this.gameObject);
    }
}
