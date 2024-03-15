using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbController : MonoBehaviour
{
    [SerializeField] List<ClimbAction> actions;
    private WallChecker wallChecker;
    public WallChecker.ClimbData climbData;
    private Animator animator;
    private bool isPerformingAnimation = false;
    private Rigidbody rb;
    [SerializeField] bool isNearWall = false;
    //[SerializeField] Vector3 heading = Vector3.zero;
    [SerializeField] float heading = 0f;
    [SerializeField] private float headingTolerant = 5f;
    void Start()
    {
        wallChecker = GetComponent<WallChecker>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        heading = transform.rotation.eulerAngles.y;
        climbData = wallChecker.WallCheck();
        isNearWall = climbData.isRayHitWallLayer;
        if (Input.GetButton("Jump") && !isPerformingAnimation && HeadingChecker())
        {
            // Do Parkour thingy here
            
            if (climbData.isRayHitWallLayer)
            {
                foreach (ClimbAction action in actions)
                {
                    if (action.CheckAction(climbData, transform))
                    {
                        StartCoroutine(ClimbingAction(action));
                        break;
                    }
                }
            } 
        } 
    }

    IEnumerator ClimbingAction(ClimbAction action)
    {
        isPerformingAnimation = true;
        PlayerController.instance.SetControl(false);
        animator.CrossFade(action.AnimationName, 0.2f);

        // Wait for the current frame to end before calling GetNextAnimatorClipInfo
        yield return null;

        AnimatorStateInfo animatinoClipInfo = animator.GetNextAnimatorStateInfo(0);
        if(!animatinoClipInfo.IsName(action.AnimationName))
        {
            Debug.LogError(string.Format("Wrong animation name: {0}", action.AnimationName));
        }

        //Debug.Log(string.Format("{0} runs for {1:0.0000}", action.AnimationName, animatinoClipInfo.length));
        //yield return new WaitForSeconds(animatinoClipInfo.length);

        float timer = 0f;

        while (timer <= animatinoClipInfo.length)
        {
            timer += Time.deltaTime;

            if (action.IsTargetMatchingEnable)
            {
                MatchTarget(action);
            }

            yield return null;
        }

        PlayerController.instance.SetControl(true);
        isPerformingAnimation = false;

    }

    private bool HeadingChecker()
    {
        float rightHeading = Quaternion.LookRotation(Vector3.right).eulerAngles.y;
        float leftHeading = Quaternion.LookRotation(Vector3.left).eulerAngles.y;

        // Calculate the difference between heading and the target angles
        // Mathf.DeltaAngle is used to ensure the heading constraint between 180 and -180 degree
        float angleDifferenceRight = Mathf.Abs(Mathf.DeltaAngle(heading, rightHeading));
        float angleDifferenceLeft = Mathf.Abs(Mathf.DeltaAngle(heading, leftHeading));

        // Check if the angle difference is within the tolerance range
        if (angleDifferenceRight <= headingTolerant || angleDifferenceLeft <= headingTolerant)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MatchTarget(ClimbAction action)
    {
        if (!animator.IsInTransition(0))
        {
            animator.MatchTarget(action.MatchPosition,
                        transform.rotation, action.MatchingTargetBodyPart,
                        new MatchTargetWeightMask(new Vector3(0, 1, 0), 0),
                        action.MatchingTargetStartPercentage, action.MatchTargetEndPercentage);
        }

        // MatchTargetWeightMask only for Y-axis hence Vector3(0, 1, 0), X and Z aren't used here, and 0 for rotation
    }

    public bool IsPerformingAnimation => isPerformingAnimation;

    public bool IsNearWall => isNearWall;
}
