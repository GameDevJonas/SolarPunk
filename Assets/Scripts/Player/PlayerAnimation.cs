using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerManager manager;
    [SerializeField] private Transform spriteObject;
    private float currentAngle;

    public float groundCheckDistance;
    public LayerMask whatIsGround;

    public bool isSquishing;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAngle = manager.movement.targetAngle;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAngles();
        GetGroundNormal();
    }

    private void GetGroundNormal()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), Vector3.down * groundCheckDistance, Color.red);
        if(Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.down, out hit, groundCheckDistance, whatIsGround))
        {
            Quaternion slopeRotation = Quaternion.FromToRotation(transform.up, hit.normal);

            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation * transform.rotation, 10 * Time.deltaTime);

            //Debug.Log(hit.normal);
        }
    }


    private void CheckForAngles()
    {
        if (currentAngle != manager.movement.targetAngle)
        {
            Flip();
            currentAngle = manager.movement.targetAngle;
        }
    }

    private void Flip()
    {
        spriteObject.localScale = new Vector3(spriteObject.localScale.x * -1, spriteObject.localScale.y, spriteObject.localScale.z);
    }

    public void PickUpObject(bool pickUp)
    {
        StartCoroutine(SquishMe());
        if (pickUp)
        {
        }
        else
        {

        }
    }

    public IEnumerator SquishMe()
    {
        isSquishing = true;
        while (spriteObject.localScale.y > .8f)
        {
            spriteObject.localScale -= new Vector3(0, .01f, 0);
            yield return new WaitForEndOfFrame();
        }
        spriteObject.localScale = new Vector3(spriteObject.localScale.x, .8f, spriteObject.localScale.z);

        yield return new WaitForSeconds(.2f);

        while (spriteObject.localScale.y < 1)
        {
            spriteObject.localScale += new Vector3(0, .01f, 0);
            yield return new WaitForEndOfFrame();
        }
        spriteObject.localScale = new Vector3(spriteObject.localScale.x, 1, spriteObject.localScale.z);

        isSquishing = false;
    }
}
