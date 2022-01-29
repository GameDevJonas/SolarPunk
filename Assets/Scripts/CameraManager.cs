using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform cam;
    private Transform player;

    [SerializeField] private LayerMask whatToObscure;

    [SerializeField] private List<GameObject> obscuredObjs = new List<GameObject>();

    private void Awake()
    {
        cam = Camera.main.transform;
        Debug.Log("Dis camera", cam);
        player = FindObjectOfType<PlayerManager>().transform.GetChild(1);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForObscurations();
    }

    private void CheckForObscurations()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (whatToObscure.Contains(other.gameObject.layer))
        {
            obscuredObjs.Add(other.gameObject);
            if (other.GetComponent<MeshRenderer>()) other.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (whatToObscure.Contains(other.gameObject.layer))
        {
            if (other.GetComponent<MeshRenderer>()) other.GetComponent<MeshRenderer>().enabled = true;
            obscuredObjs.Remove(other.gameObject);
        }
    }
}
