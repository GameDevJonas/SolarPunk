using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoldDownButton : MonoBehaviour
{
    [SerializeField] private Transform button;

    public UnityEvent onPushDownEvents;
    public UnityEvent onReleaseEvents;

    private List<GameObject> onMe = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Release();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PushDown()
    {
        button.localPosition = new Vector3(0, .2f, 0);
        onPushDownEvents.Invoke();
    }

    private void Release()
    {
        button.localPosition = new Vector3(0, .65f, 0);
        onReleaseEvents.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Pickup"))
        {
            onMe.Add(other.gameObject);
            if (onMe.Count < 2) PushDown();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Pickup"))
        {
            onMe.Remove(other.gameObject);
            if (onMe.Count < 1) Release();
        }
    }
}
