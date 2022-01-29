using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private PlayerManager player;

    private Collider myCollider;
    private Rigidbody myRb;

    public bool isPickedUp;
    private Transform targetPoint;

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
        myRb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(isPickedUp) transform.position = targetPoint.position;
    }

    public void PickMeUp(Transform holdPoint)
    {
        isPickedUp = true;
        myRb.isKinematic = true;
        myRb.isKinematic = false;
        myCollider.isTrigger = true;
        myRb.useGravity = false;
        transform.position = holdPoint.position;
        transform.SetParent(holdPoint);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void DropMe()
    {
        //transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.SetParent(null);
        isPickedUp = false;
        myCollider.isTrigger = false;
        myRb.useGravity = true;
        Vector3 distance = transform.position - player.transform.position;
        float playerDistance = distance.x;
        if (playerDistance > 0) transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z);
        else transform.position = new Vector3(transform.position.x - 0.4f, transform.position.y, transform.position.z);
    }

    public void SetStaticPosition(Transform targetPoint)
    {
        transform.position = targetPoint.position;
        isPickedUp = true;
        //this.enabled = false;
    }

    public void EnableAfterSeconds(int seconds)
    {
        Invoke("EnableMe", seconds);
    }

    private void EnableMe()
    {
        isPickedUp = false;
    }
}
