using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] List<Animator> animators;
    [SerializeField] List<GameObject> itemSpawningPoints;
    [SerializeField] List<GameObject> prefabItemsToSpawn;

    [SerializeField] Outline outline;

    private bool isOpen = false;
    private bool isOutlined = false;

    public void SetIsOutlined(bool isOutlined)
    {
        this.isOutlined = isOutlined;
        outline.OutlineWidth = (isOutlined ? 6 : 0);
    }

    private void Update()
    {
        if (isOutlined && Input.GetKeyDown(KeyCode.E))
           Interact();
    }

    private void Start()
    {
        outline.OutlineWidth = 0;
    }




    public void Interact()
    {
        isOpen = !isOpen;

        foreach (var animator in animators)
        {
            animator.SetBool("IsOpen", isOpen);
            animator.SetTrigger("Interacts");
        }

    }
}
