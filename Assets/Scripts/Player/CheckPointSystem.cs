using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public static CheckPointSystem instance = null;
    [SerializeField] private Vector3 latestCheckpoint = Vector3.zero;
    void Start()
    {
        latestCheckpoint = transform.position;
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
    }

    public void UpdateCheckpoint(Vector3 checkpoint)
    {
        latestCheckpoint = checkpoint;
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn player at " + latestCheckpoint);
        // Without Singleton, we have to do this
        //PlayerController playerController = GetComponent<PlayerController>();
        // playerController,transform.position = lasestCheckpoint;
        PlayerController.instance.transform.position = latestCheckpoint;
        Physics.SyncTransforms();

    }
}
