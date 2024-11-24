using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    int health = 100;
    int maxHealth = 100;

    int money = 0;

    int floor = 0;
    int maxFloor = 10;

    bool hasFoundButton = false;

    bool playerIsHidden = false;

    static PlayerStats instance;
    Coroutine invicibilityFramesCoroutine;


    static InputDevice device;
    static public InputDevice GetDevice()
    {
        if (device == null)
        {
            device = Keyboard.current;
        }
        return device;
    }
    static public void SetDevice(InputDevice _device)
    {
        if(_device == Mouse.current)
        {
            _device = Keyboard.current;
        }
        device = _device;
    }







    public static PlayerStats Instance()
    {
        if (instance == null)
        {
            instance = new PlayerStats();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;

        LoadFile();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }



    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        CanvaPlayerUI.Instance().UpdateHealth();
    }

    public void TakeDamage(int amount)
    {
        if (invicibilityFramesCoroutine == null)
        {
            invicibilityFramesCoroutine = StartCoroutine(InvicibilityFrames(amount, 0.5f));
        } 
        
    }
    public void AddMoney(int amount)
    {
        money += amount;
        CanvaPlayerUI.Instance().UpdateMoney();
    }
    public void RemoveMoney(int amount) {
        money -= amount;
        CanvaPlayerUI.Instance().UpdateMoney();
    }



    public void SetPlayerHidden(bool _playerIsHidden)
    {
        Debug.Log("Player is hidden: " + _playerIsHidden);
        playerIsHidden = _playerIsHidden;
    }
    public bool GetPlayerHidden()
    {
        return playerIsHidden;
    }



    public int GetHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetMoney()
    {
        return money;
    }




    public void SetHasFoundButton(bool _hasFoundButton)
    {
        hasFoundButton = _hasFoundButton;
    }
    public bool GetHasFoundButton()
    {
        return hasFoundButton;
    }


    public void NextFloor()
    {
        floor++;


        if (floor == maxFloor)
        {
            StartCoroutine(EndGame());
        }
        else
        {
            LoadSceneManager.Instance.LoadRandomScene();
        }
    }
    public int GetFloor()
    {
        return floor;
    }


    IEnumerator InvicibilityFrames(int amount, float duration)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
        CanvaPlayerUI.Instance().UpdateHealth();
        if (health <= 0)
        {
            StartCoroutine(EndGame());
        }


        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        invicibilityFramesCoroutine = null;

        
    }


    IEnumerator EndGame()
    {
        SaveFile();

        yield return new WaitForSeconds(2);


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("Death");
    }


    private void SaveFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "saveFile.txt");

        if (floor != 10)
        {
            money /= 2;
        }

        string data = $"{money.ToString()}";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLine(money.ToString());
            }
    }






    private void LoadFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "saveFile.txt");
        if (!File.Exists(path))
        {
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.WriteLine("0");
                }
        }



        using (StreamReader reader = new StreamReader(path))
        {
            string data = reader.ReadLine();
            money = int.Parse(data);
        }
    }
}
