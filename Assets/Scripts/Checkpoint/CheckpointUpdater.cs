using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointUpdater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //CheckPointSystem checkPointSystem = transform.parent.GetComponent<CheckPointSystem>();
            //checkPointSystem.UpdateCheckpoint(transform.position);
            CheckPointSystem.instance.UpdateCheckpoint(transform.position);

        }
    }
}
