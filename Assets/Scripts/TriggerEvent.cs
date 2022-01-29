using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public GameObject target;

    public UnityEvent onTrigger;

    public bool doOnce;
    private bool executed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject == target)
        {
            Debug.Log("Trigger entered by object");
            if (doOnce && !executed)
            {
                onTrigger.Invoke();
                executed = true;
            }
            else if (!doOnce)
            {
                onTrigger.Invoke();
                print("Invoked");
            }
        }
    }
}
