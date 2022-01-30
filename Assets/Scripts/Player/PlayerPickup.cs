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

    [Header("Audio")]
    [SerializeField] private AudioSource pickupSource;
    [SerializeField] private AudioSource dropSource;
    [SerializeField] private AudioClip[] pickupClips, dropClips;


    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
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

    public void DropObject()
    {
        dropSource.clip = dropClips[Random.Range(0, dropClips.Length)];
        dropSource.pitch = Random.Range(.8f, 1.2f);
        dropSource.Play();

        isHolding = false;
        manager.StartCoroutine(manager.PickUpInputs());
        manager.anim.PickUpObject(false);
        activePickup.DropMe();
        activePickup = null;
    }

    private void PickUpObject(Pickup pickupObject)
    {
        pickupSource.clip = pickupClips[Random.Range(0, pickupClips.Length)];
        pickupSource.pitch = Random.Range(.8f, 1.2f);
        pickupSource.Play();

        isHolding = true;
        manager.StartCoroutine(manager.PickUpInputs());
        manager.anim.PickUpObject(false);
        activePickup = pickupObject;
        activePickup.PickMeUp(holdPoint);
        pickupCollider.pickupsInRange.Remove(pickupObject);
    }
}
