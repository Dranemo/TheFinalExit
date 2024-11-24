using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private int poolSize = 10;
    [SerializeField] private GameObject audioSourcePrefab;

    private Queue<AudioSource> audioSources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        audioSources = new Queue<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject audioObject = Instantiate(audioSourcePrefab, transform);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSources.Enqueue(audioSource);
        }
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        if (audioSources.Count > 0)
        {
            AudioSource audioSource = audioSources.Dequeue();
            audioSource.transform.position = position;
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(ReturnToPool(audioSource, clip.length));
        }
    }

    private IEnumerator ReturnToPool(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSources.Enqueue(audioSource);
    }
}
