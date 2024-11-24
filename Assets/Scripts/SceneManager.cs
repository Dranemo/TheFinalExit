using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] AudioClip elevatorSound;
    [SerializeField] Animator animatorElevatorDoor;
    Scene? loadedScene;
    public static LoadSceneManager Instance;

    private float durationWait = 2f;

    enum Scenes
    { 
        Floor1,
        Floor2,
        Floor3,
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        LoadRandomScene();
    }




    public void LoadRandomScene()
    {
        int random = UnityEngine.Random.Range(0, Enum.GetValues(typeof(Scenes)).Length);
        string randomFloor = Enum.GetValues(typeof(Scenes)).GetValue(random).ToString();

        StartCoroutine(UnLoadSceneAsync(randomFloor));
    }


    public IEnumerator UnLoadSceneAsync(string name, bool isEnd = false)
    {
        if (loadedScene.HasValue && loadedScene.Value.IsValid())
        {
            GameObject floorManager = FloorManagerSingleton.floorManager;
            floorManager.GetComponent<FloorMonsterSpawner>().DeleteMonsters();
            floorManager.GetComponent<SpawnButtonElevator>().DeleteButton();


            animatorElevatorDoor.GetComponent<BoxCollider>().enabled = true;

            animatorElevatorDoor.SetBool("IsOpen", false);
            animatorElevatorDoor.SetTrigger("Interacts");

            while (!animatorElevatorDoor.GetCurrentAnimatorStateInfo(0).IsName("IdleClosed"))
            {
                yield return null;
            }


            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(loadedScene.Value);

            float t = 0;
            while (t < durationWait / 2)
            {
                t += Time.deltaTime;
                yield return null;
            }

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            
        }
        else
        {
            Debug.Log("La scène n'est pas valide ou n'a pas été chargée.");
        }

        if(!isEnd)
            StartCoroutine(LoadSceneAsync(name));
    }

    IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        float t = 0;
        while (t < durationWait / 2)
        {
            t += Time.deltaTime;
            yield return null;
        }

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        loadedScene = SceneManager.GetSceneByName(name);

        

        animatorElevatorDoor.GetComponent<BoxCollider>().enabled = false;
        AudioManager.Instance.PlaySound(elevatorSound, animatorElevatorDoor.transform.position);
        animatorElevatorDoor.SetBool("IsOpen", true);
        animatorElevatorDoor.SetTrigger("Interacts");

    }
}
