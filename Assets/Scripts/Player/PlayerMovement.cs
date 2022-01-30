using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerManager manager;

    private CharacterController controller;

    [SerializeField] private float speed;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float verticalSpeed = 0;

    public bool isGrounded;

    public float targetAngle;

    public float horizontalAxis;
    public float verticalAxis;
    private Vector3 direction;

    public bool isMoving;

    public Vector3 hitNormal;
    public LayerMask whatIsGround;

    public bool disableInputs;
    private bool moveToTarget;
    private Transform movingTarget;
    private float moveTimer;

    [SerializeField] private float footStepInterval;
    private float footStepTimer;
    [SerializeField] private AudioSource footStepSource;
    [SerializeField] private AudioClip[] footStepClips;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        controller = GetComponent<CharacterController>();
        disableInputs = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disableInputs) GetInputs();
        ApplyGravity();
        ApplyMovement();

        if (moveToTarget) MovingToTarget();

        if (isMoving) FootStepCalc();
    }

    private void FootStepCalc()
    {
        if(footStepTimer <= 0)
        {
            footStepSource.pitch = Random.Range(.8f, 1.2f);
            footStepSource.clip = footStepClips[Random.Range(0, footStepClips.Length)];
            footStepSource.Play();
            footStepTimer = footStepInterval;
        }
        else
        {
            footStepTimer -= Time.deltaTime;
        }
    }

    private void GetInputs()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        isMoving = (horizontalAxis != 0 || verticalAxis != 0);

        moveTimer = 0;
    }

    private void ApplyMovement()
    {
        if (direction.magnitude >= 0.1f)
        {
            if (horizontalAxis != 0) targetAngle = Mathf.Atan2(direction.x, 0) * Mathf.Rad2Deg;

            controller.Move(direction * speed * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        RaycastHit hit;
        bool ray = Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.down, out hit, manager.anim.groundCheckDistance, whatIsGround);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), Vector3.down * manager.anim.groundCheckDistance, Color.black);
        isGrounded = ((Vector3.Angle(Vector3.up, hitNormal) <= controller.slopeLimit) && ray);

        //Debug.Log(Vector3.Angle(Vector3.up, hitNormal));

        if (isGrounded) verticalSpeed = 0;

        verticalSpeed -= gravity * Time.deltaTime;

        direction.y = verticalSpeed;
    }

    public void MoveToPoint(Transform target)
    {
        disableInputs = true;
        moveToTarget = true;
        movingTarget = target;
    }

    private void MovingToTarget()
    {
        Vector3 targetPoint = Vector3.MoveTowards(transform.position, movingTarget.position, Mathf.Infinity).normalized;
        direction = new Vector3(targetPoint.x, 0f, targetPoint.z).normalized;
        float distance = Vector3.Distance(transform.position, movingTarget.position);
        if(distance < 1f || moveTimer > 1)
        {
            disableInputs = false;
            moveToTarget = false;
        }
        moveTimer += Time.deltaTime;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }
}
