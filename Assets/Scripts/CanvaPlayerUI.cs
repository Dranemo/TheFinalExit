using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaPlayerUI : MonoBehaviour
{
    [SerializeField] GameObject invSquarePrefab;

    [SerializeField] HorizontalLayoutGroup invPanel;

    List<GameObject> squares = new List<GameObject>();








    Inventory inventory;
    PlayerStats playerStats;


    private void Start()
    {
        inventory = Inventory.Instance();
        playerStats = PlayerStats.Instance();
    }



    public void UpdateSquares()
    {
        foreach (var square in squares)
        {
            Destroy(square);
        }
        squares.Clear();

        for (int i = 0; i < inventory.GetInventory().Count; i++)
        {
            GameObject square = Instantiate(invSquarePrefab, invPanel.transform);
            

            squares.Add(square);
        }
    }
    public void UpdateSelectedItem(int index)
    {
        for (int i = 0; i < squares.Count; i++)
        {
            if (i == index)
            {
                squares[i].GetComponent<SquareInvUI>().SetSelected(true);
            }
            else
            {
                squares[i].GetComponent<SquareInvUI>().SetSelected(false);
            }
        }
    }

}
