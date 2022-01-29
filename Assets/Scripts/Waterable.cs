using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterable : MonoBehaviour
{
    private float progress = 0;
    public float increaseValue;

    public bool grown;

    [SerializeField] private Transform growObj;

    // Start is called before the first frame update
    void Start()
    {
        growObj.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!grown) growObj.localScale = Vector3.one * progress / 100;
        if(grown) growObj.localScale = Vector3.one;
    }

    public void GrowMe()
    {
        if (progress <= 100) progress += increaseValue * Time.deltaTime;
        if(progress >= 100)
        {
            grown = true;
            growObj.localScale = Vector3.one;
        }
    }
}
