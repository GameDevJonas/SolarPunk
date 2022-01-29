using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Waterable : MonoBehaviour
{
    public float waterValue;

    public bool grown;

    public UnityEvent onGrow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GrowMe()
    {
        grown = true;
        onGrow.Invoke();
    }
}
