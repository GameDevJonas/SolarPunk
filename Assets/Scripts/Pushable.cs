using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    private PlayerManager player;

    public enum PushDirection { left, right, both };
    public PushDirection pushDirection;

    public float pushForce;

    public Vector2 playerPos;

    public bool playerInRange;
    public bool playerPushing;

    private CharacterController controller;

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && player.sunEnergy.sunEnergy > 0)
        {
            CheckForPlayer();
            CheckIfPlayerPush();
        }
    }

    private void FixedUpdate()
    {
        //if(playerInRange) CheckIfPlayerPush();
    }

    private void CheckForPlayer()
    {
        Vector3 playerDiff = player.transform.position - transform.position;
        playerPos = new Vector2((int)playerDiff.x, (int)playerDiff.z).normalized;
    }

    private void CheckIfPlayerPush()
    {
        playerPushing = (player.movement.isMoving);
        Vector3 translation = Vector3.zero;
        switch (pushDirection)
        {
            case PushDirection.left:
                if (player.movement.horizontalAxis < 0 && playerPos.x > 0)
                {
                    print("Push left");
                    translation = Vector3.left * pushForce * Time.deltaTime;
                }
                break;
            case PushDirection.right:
                if (player.movement.horizontalAxis > 0 && playerPos.x < 0)
                {
                    print("Push right");
                    translation = Vector3.right * pushForce * Time.deltaTime;
                }
                break;
            case PushDirection.both:
                if (player.movement.horizontalAxis < 0 && playerPos.x > 0)
                {
                    print("Push left");
                    translation = Vector3.left * pushForce * Time.deltaTime;
                }
                else if (player.movement.horizontalAxis > 0 && playerPos.x < 0)
                {
                    print("Push right");
                    translation = Vector3.right * pushForce * Time.deltaTime;
                }
                break;
        }

        if (playerPushing)
        {
            if (!player.sunEnergy.usingEnergy) player.sunEnergy.StartPushBox();
            controller.Move(translation);
        }
        else
        {
            if (player.sunEnergy.usingEnergy) player.sunEnergy.StopPushingBox();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player.sunEnergy.StopPushingBox();
        }
    }
}
