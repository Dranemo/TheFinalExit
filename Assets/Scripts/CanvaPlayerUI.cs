using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaPlayerUI : MonoBehaviour
{
    [SerializeField] GameObject invSquarePrefab;

    [SerializeField] HorizontalLayoutGroup invPanel;
    List<GameObject> squares = new List<GameObject>();

    [SerializeField] Dictionary<Inventory.ItemType, Sprite> sprites;


    [SerializeField] GameObject Player;

    Inventory inventory;
    PlayerStats playerStats;


    private void Start()
    {
        inventory = Player.GetComponent<Inventory>();
        playerStats = PlayerStats.Instance();
    }



    public void UpdateSquares()
    {
        foreach (GameObject square in squares)
        {
            Destroy(square);
        }
        squares.Clear();

        foreach (ItemUsable item in inventory.GetInventory())
        {
            GameObject square = Instantiate(invSquarePrefab, invPanel.transform);
            square.transform.SetParent(invPanel.transform, false);

            square.GetComponent<Image>().sprite = sprites[item.itemType];
            squares.Add(square);
        }
    }


}
