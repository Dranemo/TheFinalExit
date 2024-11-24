using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManagerSingleton : MonoBehaviour
{
    public static GameObject floorManager;

    private void Awake()
    {
        if (floorManager == null)
        {
            floorManager = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
