using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerAnimation anim;
    public PlayerPickup pickup;
    public PlayerSunEnergy sunEnergy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PickUpInputs()
    {
        movement.enabled = false;
        yield return new WaitForSeconds(.5f);
        movement.enabled = true;
    }
}
