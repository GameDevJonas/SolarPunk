using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerManager manager;

    private CharacterController controller;

    [SerializeField] private float speed;

    public float targetAngle;

    private float horizontalAxis;
    private float verticalAxis;
    private Vector3 direction;

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
            if (horizontalAxis != 0) targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
