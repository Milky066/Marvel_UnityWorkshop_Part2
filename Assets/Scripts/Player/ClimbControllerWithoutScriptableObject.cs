using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbControllerWithoutScriptableObject : MonoBehaviour
{
    [Header("Animation 1")]
    [SerializeField] string animationName;
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;

    [Header("Target Matching")]
    [SerializeField] bool isTargetMatchingEnable = true;
    [SerializeField] AvatarTarget matchingTargetBodyPart;
    [SerializeField] float matchingTargetStartPercentage;
    [SerializeField] float matchingTargetEndPercentage;


    [Header("Animation 2")]
    [SerializeField] string animationName2;
    [SerializeField] float minHeight2;
    [SerializeField] float maxHeight2;

    [Header("Target Matching")]
    [SerializeField] bool isTargetMatchingEnable2 = true;
    [SerializeField] AvatarTarget matchingTargetBodyPart2;
    [SerializeField] float matchingTargetStartPercentage2;
    [SerializeField] float matchingTargetEndPercentage2;
    [Header("Animation 3")]
    [SerializeField] string animationName3;
    [SerializeField] float minHeight3;
    [SerializeField] float maxHeight3;

    [Header("Target Matching")]
    [SerializeField] bool isTargetMatchingEnable3 = true;
    [SerializeField] AvatarTarget matchingTargetBodyPart3;
    [SerializeField] float matchingTargetStartPercentage3;
    [SerializeField] float matchingTargetEndPercentage3;
    
}
