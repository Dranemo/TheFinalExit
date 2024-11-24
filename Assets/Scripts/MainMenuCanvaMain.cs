using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvaMain : MonoBehaviour
{
    [SerializeField] Button Play;
    [SerializeField] Button Quit;
    [SerializeField] Button Credits;

    [SerializeField] GameObject CreditsPanel;

    void Start()
    {
        Play.onClick.AddListener(PlayGame);
        Quit.onClick.AddListener(QuitGame);
        Credits.onClick.AddListener(ShowCredits);
    }



    void PlayGame()
    {
        Debug.Log("Play Game");
        SceneManager.LoadScene("Elevator");
    }

    void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    void ShowCredits()
    {
        Debug.Log("Show Credits");
        this.gameObject.SetActive(false);
        CreditsPanel.SetActive(true);
    }
}
