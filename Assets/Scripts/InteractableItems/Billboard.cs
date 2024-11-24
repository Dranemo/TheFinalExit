using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera camera;
    [SerializeField] List<GameObject> sprite;



    private void Start()
    {
        camera = Camera.main;
        Debug.Log(camera);
    }



    // Update is called once per frame
    void Update()
    {
        foreach (var _sprite in sprite)
            _sprite.transform.rotation = camera.transform.rotation;
    }
}
