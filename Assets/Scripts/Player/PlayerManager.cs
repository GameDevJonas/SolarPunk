using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    public IEnumerator SuckWater(float waitTime)
    {
        movement.enabled = false;
        pickup.enabled = false;
        float timer = 0;
        while (timer <= waitTime)
        {
            if (!anim.isSquishing) anim.StartCoroutine(anim.SquishMe());
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //yield return new WaitForSeconds(waitTime);
        movement.enabled = true;
        pickup.enabled = true;
    }

    public IEnumerator PickUpInputs()
    {
        movement.enabled = false;
        yield return new WaitForSeconds(.5f);
        movement.enabled = true;
    }
}
