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

    private float horizontalAxis;
    private float verticalAxis;
    private Vector3 direction;

    public Vector3 hitNormal;
    public LayerMask whatIsGround;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        ApplyGravity();
        ApplyMovement();
    }

    private void GetInputs()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;
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

        Debug.Log(Vector3.Angle(Vector3.up, hitNormal));

        if (isGrounded) verticalSpeed = 0;

        verticalSpeed -= gravity * Time.deltaTime;

        direction.y = verticalSpeed;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }
}
