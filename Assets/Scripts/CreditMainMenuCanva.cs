using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditMainMenuCanva : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] Button back;

    void Start()
    {
        back.onClick.AddListener(BackToMainMenu);
    }

    void BackToMainMenu()
    {
        Debug.Log("Back to Main Menu");
        this.gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }
}
