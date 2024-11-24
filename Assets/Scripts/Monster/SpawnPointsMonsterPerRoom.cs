using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsMonsterPerRoom : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPoints;

    public List<GameObject> GetSpawnPoints()
    {
        return spawnPoints;
    }
}
