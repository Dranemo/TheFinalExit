using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvaEnd : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI TextCoins;
    [SerializeField] TextMeshProUGUI TextFloor;

    PlayerStats playerStats;
    bool won = false;



    private void Awake()
    {
        playerStats = PlayerStats.Instance();
        button.onClick.AddListener(EndGame);


        if(playerStats.GetFloor() == 10)
        {
            won= true;
            TextFloor.text = "F�licitation ! Tu as echapp� � l'hotel !";
        }
        TextFloor.text = $"Perdu... \nEtage: {playerStats.GetFloor().ToString()}";

        TextCoins.text = $"Tu as r�colt� : {playerStats.GetMoney().ToString()} Pi�ce !";

        if(!won)
        {
            TextCoins.text += "\n Malheureusement, tu n'as pas r�ussi � t'�chapper et tu as perdu la moiti� de tes gains...";
        }
    }




    void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(playerStats.gameObject);
    }




}
