using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RequiredObject : MonoBehaviour
{
    private PlayerPickup player;

    public string requiredObjectName;

    public UnityEvent onDeliver;

    private bool playerInRange;
    private bool hasRequiredObject;

    private bool executed;

    private void Awake()
    {
        player = FindObjectOfType<PlayerPickup>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (executed) this.enabled = false;

        if (playerInRange && player.activePickup) CheckWhatPlayerHolding();
        else hasRequiredObject = false;

        if (Input.GetKeyDown(KeyCode.E) && hasRequiredObject)
        {
            //executed = true;
            Invoke("DeliverIt", 0f);
        }
    }

    private void DeliverIt()
    {
        player.DropObject();
        onDeliver.Invoke();
    }

    private void CheckWhatPlayerHolding()
    {
        hasRequiredObject = (player.activePickup.name == requiredObjectName);
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
        }
    }
}
