using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static WallChecker;

[CreateAssetMenu(menuName = "Parkour System/New Parkour Action")]
public class ClimbAction : ScriptableObject
{
    [SerializeField] string animationName;
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;

    [Header("Target Matching")]
    [SerializeField] bool isTargetMatchingEnable = true;
    [SerializeField] AvatarTarget matchingTargetBodyPart;
    [SerializeField] float matchingTargetStartPercentage;
    [SerializeField] float matchingTargetEndPercentage;

    private Vector3 matchPosition;

    public bool CheckAction(ClimbData hit, Transform player)
    {
        float height = hit.heightData.point.y - player.transform.position.y;
        Debug.Log(string.Format("Height: {0}", height));
        if(height <= maxHeight && height > minHeight)
        {
            //Debug.Log(string.Format("Climbing {0}, Height {1}", hit.hitData.transform.name, height));
            if (isTargetMatchingEnable)
            {
                matchPosition = hit.heightData.point;
            }
            return true;
        } else
        {
            return false;
        }
        
    }

    public string AnimationName => animationName;
    public Vector3 MatchPosition => matchPosition;
    public AvatarTarget MatchingTargetBodyPart => matchingTargetBodyPart;
    public float MatchingTargetStartPercentage => matchingTargetStartPercentage;
    public float MatchTargetEndPercentage => matchingTargetEndPercentage;
    public bool IsTargetMatchingEnable => isTargetMatchingEnable;
}
