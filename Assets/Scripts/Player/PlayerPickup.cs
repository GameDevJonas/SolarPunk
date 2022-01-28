using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private Pickup activePickup;
    [SerializeField] private SphereCollider pickupCollider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (activePickup && !CheckForPickupInRange()) DropObject();
            else if (!activePickup && CheckForPickupInRange()) PickUpObject(CheckForPickupInRange());
        }
    }


    private Pickup CheckForPickupInRange()
    {
        Pickup currentObject = null;

        

        return currentObject;
    }

    private void DropObject()
    {

    }

    private void PickUpObject(Pickup pickupObject)
    {

    }
}
