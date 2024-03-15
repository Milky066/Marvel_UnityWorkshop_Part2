using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointUpdater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log(CheckPointSystem.instance.gameObject.name);
            Debug.Log(string.Format("Update {0} at {1}", gameObject.name, transform.position));
            CheckPointSystem.instance.UpdateCheckpoint(transform.position);
        }
    }
}
