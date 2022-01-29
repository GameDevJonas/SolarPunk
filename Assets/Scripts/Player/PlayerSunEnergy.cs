using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSunEnergy : MonoBehaviour
{
    [Range(0, 100)] public float sunEnergy;
    [SerializeField] private float increaseValue;

    public bool inSun;

    [Header("UI")]
    [SerializeField] private Image fillImage;


    private void Awake()
    {
        fillImage = GameObject.Find("SunEnergyFront").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inSun) InSunlight();
        UpdateUI();
    }

    private void InSunlight()
    {
        if (sunEnergy < 100) sunEnergy += increaseValue * Time.deltaTime;
        if (sunEnergy > 100) sunEnergy = 100;
    }

    private void UpdateUI()
    {
        fillImage.fillAmount = sunEnergy / 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SunSpot"))
        {
            inSun = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SunSpot"))
        {
            inSun = false;
        }
    }
}
