using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private PlayerManager manager;

    public Pickup activePickup;
    [SerializeField] private Transform holdPoint;
    public PickupTrigger pickupCollider;

    public bool isHolding;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

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
            if (activePickup && !CheckForPickupInRange() && !CheckForWall()) DropObject();
            else if (!activePickup && CheckForPickupInRange()) PickUpObject(CheckForPickupInRange());
        }
    }


    private Pickup CheckForPickupInRange()
    {
        Pickup currentObject = null;

        if (pickupCollider.pickupsInRange.Count > 0) currentObject = pickupCollider.pickupsInRange[0];

        return currentObject;
    }

    private bool CheckForWall()
    {
        if (pickupCollider.objects.Count > 0) return true;
        return false;
    }

    private void DropObject()
    {
        isHolding = false;
        manager.StartCoroutine(manager.PickUpInputs());
        manager.anim.PickUpObject(false);
        activePickup.DropMe();
        activePickup = null;
    }

    private void PickUpObject(Pickup pickupObject)
    {
        isHolding = true;
        manager.StartCoroutine(manager.PickUpInputs());
        manager.anim.PickUpObject(false);
        activePickup = pickupObject;
        activePickup.PickMeUp(holdPoint);
        pickupCollider.pickupsInRange.Remove(pickupObject);
    }
}
