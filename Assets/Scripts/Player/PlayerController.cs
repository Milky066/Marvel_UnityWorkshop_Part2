using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerController instance;
    [SerializeField] private float speed = 3f;
    [SerializeField] private bool loadProgress = false;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private LayerMask groundLayer;

    private Quaternion targetRotation;
    private Animator animator;
    private CharacterController characterController;
    private bool isPlayerGrounded = false;
    private bool hasControl = true;
    [SerializeField] private float fallSpeed = 0f;
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
        animator = GetComponent<Animator>();
        characterController = gameObject.GetComponent<CharacterController>();
        if (loadProgress)
        {
            PlayerSaveLoader.LoadProgressJson();
        }

    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float movementAmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        movementAmount = Mathf.Clamp(movementAmount, 0, 1);
        Vector3 movementInput = new Vector3(horizontal, 0, 0).normalized;
        Vector3 moveDirection = movementInput;
        if (hasControl)
        {
            isPlayerGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
            if (isPlayerGrounded)
            {
                fallSpeed = -0.5f;
            }
            else
            {
                fallSpeed += Physics.gravity.y * Time.deltaTime;
            }
            moveDirection.y = fallSpeed;
            characterController.Move(moveDirection * speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            if (horizontal > 0)
            {
                targetRotation = Quaternion.LookRotation(Vector3.right);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500f * Time.deltaTime);
            } else if(horizontal < 0)
            {
                targetRotation = Quaternion.LookRotation(Vector3.left);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500f * Time.deltaTime);
            } 

            animator.SetFloat("movementAmount", movementAmount, 0.2f, Time.deltaTime);
        }
        
    }

    public bool IsPlayerGrounded => isPlayerGrounded;
    public void SetControl(bool hasControl)
    {
        this.hasControl = hasControl;
        characterController.enabled = hasControl;

        if (!hasControl)
        {
            // Reset Movement and target rotation
            animator.SetFloat("movementAmount", 0f);
            targetRotation = transform.rotation;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = isPlayerGrounded ? Color.green : Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

}
