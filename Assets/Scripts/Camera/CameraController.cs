using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance = null;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 framingOffset = Vector3.zero;
    [SerializeField] private float cameraFollowSpeed = 3f;

    [Header("Curent Rotations")]
    [SerializeField] private float xRotation = 0f;
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Update()
    {
        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position + framingOffset, cameraFollowSpeed * Time.deltaTime);
    }

    public Quaternion GetPlanarRotation()
    {
        // aka horizontal rotation
        return Quaternion.Euler(0, xRotation, 0);
    }
}
