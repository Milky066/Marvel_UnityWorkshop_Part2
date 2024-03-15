using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WallChecker;

public class WallChecker : MonoBehaviour
{
    [SerializeField] private Vector3 forwardRayOffset = Vector3.zero;
    [SerializeField] private float raycastDistance = 2.0f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float climbHeightLimit = 5f;
    // Start is called before the first frame update
    public struct ClimbData
    {
        public bool isRayHitWallLayer;
        public bool isHeightHit;
        public RaycastHit hitData;
        public RaycastHit heightData;
    }

    public ClimbData WallCheck()
    {
        ClimbData climbData = new ClimbData();

        //Horizontal Raycast for checking wall
        climbData.isRayHitWallLayer = Physics.Raycast(transform.position + forwardRayOffset, transform.forward, out climbData.hitData, raycastDistance, wallLayer);
        Debug.DrawRay(transform.position + forwardRayOffset, transform.forward * raycastDistance, climbData.isRayHitWallLayer ? Color.red : Color.green);

        //Vertical Raycast for checking grab point
        if (climbData.isRayHitWallLayer)
        {
            // Casting a ray down from above
            Vector3 hitPoint = climbData.hitData.point + Vector3.up * climbHeightLimit;
            climbData.isHeightHit = Physics.Raycast(hitPoint, Vector3.down * climbHeightLimit, out climbData.heightData, wallLayer);
            Debug.DrawRay(hitPoint, Vector3.down * climbHeightLimit, climbData.isHeightHit ? Color.green : Color.red);
        }

        return climbData;
    }


}
