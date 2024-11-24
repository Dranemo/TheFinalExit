using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningItemScript : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] List<GameObject> spawnPointsOutside;



    bool hasMadeSoundWhenOpened = true;
    public bool GetHasMadeSoundWhenOpened()
    {
        return hasMadeSoundWhenOpened;
    }
    public void HasMadeSound()
    {
        hasMadeSoundWhenOpened = false;
    }


    private void Start()
    {
        foreach (var spawnpoint in spawnPoints)
        {
            SpawnItem(spawnpoint);
        }
        foreach (var spawnpoint in spawnPointsOutside)
        {
            SpawnItem(spawnpoint, true);
        }
    }


    public void SpawnItem(GameObject spawnpoint, bool isOutside = false)
    {
        int random = UnityEngine.Random.Range(0, 4);

        // Il y a un item si random = 0                                         25%
        if(random == 0)
        {
            hasMadeSoundWhenOpened = false;

            random = UnityEngine.Random.Range(0, 5);

            // Il y a des coins si random < 3                                   40%
            if (random < 2)
            {
                random = UnityEngine.Random.Range(0, 10);

                // Petite pile de coins si random < 6                            60%
                if (random < 6)
                {
                    ItemSpawner.SpawnObject(ItemSpawner.CoinPileType.CoinPile.ToString(), spawnpoint.transform.position, Quaternion.Euler(new Vector3(Quaternion.identity.x, UnityEngine.Random.Range(0, 180), Quaternion.identity.z)), this.gameObject);
                }
                // Moyenne pile de coins si random < 9                           30%
                else if (random < 9) 
                {
                    ItemSpawner.SpawnObject(ItemSpawner.CoinPileType.CoinPile2.ToString(), spawnpoint.transform.position, Quaternion.Euler(new Vector3(Quaternion.identity.x, UnityEngine.Random.Range(0, 180), Quaternion.identity.z)), this.gameObject);
                }
                // Grande pile de coins si random < 10                           10%
                else
                {
                    ItemSpawner.SpawnObject(ItemSpawner.CoinPileType.CoinPile3.ToString(), spawnpoint.transform.position, Quaternion.Euler(new Vector3(Quaternion.identity.x, UnityEngine.Random.Range(0, 180), Quaternion.identity.z)), this.gameObject);
                }
            }
            // Il y a des consummables si random < 4                            40%
            else if (random < 4)
            {
                random = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ItemSpawner.ConsumableItemType)).Length);
            }
            // Il y a des usables si random < 5                                 20%
            else
            {
                random = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ItemSpawner.UsableItemType)).Length);

                ItemSpawner.SpawnObject(Enum.GetValues(typeof(ItemSpawner.UsableItemType)).GetValue(random).ToString(), spawnpoint.transform.position, Quaternion.Euler(new Vector3(Quaternion.identity.x, UnityEngine.Random.Range(0, 180), Quaternion.identity.z)), this.gameObject);

            }


        }
        else if(isOutside)
        {
            random = UnityEngine.Random.Range(0, 4);

            if(random == 0)
            {
                random = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ItemSpawner.DecorationItemType)).Length);
            }
        }
    }
}
