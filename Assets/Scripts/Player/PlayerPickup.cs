using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private Pickup activePickup;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private PickupTrigger pickupCollider;

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

        if (pickupCollider.pickupsInRange.Count > 0) currentObject = pickupCollider.pickupsInRange[0];

        return currentObject;
    }

    private void DropObject()
    {
        activePickup.DropMe();
        activePickup = null;
    }

    private void PickUpObject(Pickup pickupObject)
    {
        activePickup = pickupObject;
        activePickup.PickMeUp(holdPoint);
        pickupCollider.pickupsInRange.Remove(pickupObject);
    }
}