using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMonsterSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> rooms;
    List<GameObject> monstersOnScene = new List<GameObject>();

    [SerializeField] GameObject noeilsPrefab;
    [SerializeField] GameObject pierrePrefab;

    bool canSpawnPierre = false;
    float durationCoolDownPierre = 90f;
    float durationCoolDownPierreStart = 10f;


    PlayerStats playerStats;
    int floorNumber;

    private void Start()
    {
        playerStats = PlayerStats.Instance();
        floorNumber = playerStats.GetFloor() + 1;

        SpawnNoeuils();
        StartCoroutine(WaitForCoolDownPierre(durationCoolDownPierreStart));
    }

    private void Update()
    {
        SpawnPierre();
    }




    private void SpawnNoeuils()
    {
        int random = Random.Range(1, 31);

        // SpawnNoeils si random <= floorNumber * 2
        if (random <= floorNumber * 2)
        {
            Debug.Log("SpawnNoeuils");

            random = Random.Range(0, rooms.Count);
            GameObject room = rooms[random];

            random = Random.Range(0, room.GetComponent<SpawnPointsMonsterPerRoom>().GetSpawnPoints().Count);
            GameObject spawnPoint = room.GetComponent<SpawnPointsMonsterPerRoom>().GetSpawnPoints()[random];

            GameObject noeuils = Instantiate(noeilsPrefab, spawnPoint.transform.position, Quaternion.identity);
            monstersOnScene.Add(noeuils);
        }
    }

    private void SpawnPierre()
    {
        if (canSpawnPierre)
        {
            int random = Random.Range(1, 100001);

            // SpawnPierre si random <= floorNumber * 1.5
            if (random <= floorNumber * 10f)
            {
                random = Random.Range(0, rooms.Count);
                GameObject room = rooms[random];

                Debug.Log("SpawnPierre");


                if(!room.GetComponent<LightsOffGestion>().lightsOff)
                {
                    canSpawnPierre = false;
                    StartCoroutine(SpawningPierre(room));
                }
            }
        }
    }


    IEnumerator SpawningPierre(GameObject room)
    {
        int random = Random.Range(0, room.GetComponent<SpawnPointsMonsterPerRoom>().GetSpawnPoints().Count);
        GameObject spawnPoint = room.GetComponent<SpawnPointsMonsterPerRoom>().GetSpawnPoints()[random];

        LightsOffGestion lightsOffGestion = room.GetComponent<LightsOffGestion>();
        lightsOffGestion.FlashingLights(10f);
        yield return new WaitForSeconds(5f);


        GameObject pierre = Instantiate(pierrePrefab, spawnPoint.transform.position, Quaternion.identity);

        monstersOnScene.Add(pierre);

        yield return new WaitForSeconds(5f);

        monstersOnScene.Remove(pierre);
        Destroy(pierre);

        lightsOffGestion.TurnOffLights();
        StartCoroutine(WaitForCoolDownPierre(durationCoolDownPierre));
    }

    IEnumerator WaitForCoolDownPierre(float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            yield return null;
        }
        canSpawnPierre = true;
    }

    public void DeleteMonsters()
    {
        foreach (GameObject monster in monstersOnScene)
        {
            Destroy(monster);
        }
    }
}
