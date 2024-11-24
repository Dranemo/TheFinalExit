using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawnPoints : MonoBehaviour
{
    [SerializeField] Direction direction;

    public enum Direction
    {
        X,
        Z,
        mX,
        mZ
    }


    public Direction GetDirection()
    {
        return direction;
    }
}
