using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButtonElevator : MonoBehaviour
{
    [SerializeField] GameObject buttonElevatorPrefab;
    GameObject button;

    [SerializeField] List<GameObject> spawnPoints;

    Dictionary<ButtonSpawnPoints.Direction, Vector3> directionToVector = new Dictionary<ButtonSpawnPoints.Direction, Vector3>
    {
        { ButtonSpawnPoints.Direction.X, Vector3.back * 90 },
        { ButtonSpawnPoints.Direction.Z, Vector3.right * 90},
        { ButtonSpawnPoints.Direction.mX, Vector3.forward * 90 },
        { ButtonSpawnPoints.Direction.mZ, Vector3.left * 90}
    };


    private void Start()
    {
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        button = Instantiate(buttonElevatorPrefab, spawnPoint.transform.position, Quaternion.Euler(directionToVector[spawnPoint.GetComponent<ButtonSpawnPoints>().GetDirection()]));
    }


    public void DeleteButton()
    {
        Destroy(button);
    }
}
