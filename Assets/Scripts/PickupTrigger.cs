using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    public List<Pickup> pickupsInRange = new List<Pickup>();
    public List<Water> water = new List<Water>();
    public List<GameObject> objects = new List<GameObject>();
    public List<Waterable> waterables = new List<Waterable>();

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
        if (other.CompareTag("Pickup"))
        {
            pickupsInRange.Add(other.GetComponent<Pickup>());
        }
        if (other.CompareTag("Wall"))
        {
            objects.Add(other.gameObject);
        }
        if (other.CompareTag("Water"))
        {
            water.Add(other.GetComponent<Water>());
        }
        if (other.CompareTag("Waterable"))
        {
            waterables.Add(other.GetComponent<Waterable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            pickupsInRange.Remove(other.GetComponent<Pickup>());
        }
        if(other.CompareTag("Wall"))
        {
            objects.Remove(other.gameObject);
        }
        if (other.CompareTag("Water"))
        {
            water.Remove(other.GetComponent<Water>());
        }
        if (other.CompareTag("Waterable"))
        {
            waterables.Remove(other.GetComponent<Waterable>());
        }
    }
}
