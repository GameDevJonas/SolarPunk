using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSunEnergy : MonoBehaviour
{
    [Range(0, 100)] public float sunEnergy;
    [SerializeField] private float increaseValue;
    [SerializeField] private float decreaseValue;

    public bool inSun;

    public bool usingEnergy;

    [Header("UI")]
    [SerializeField] private Image fillImage;

    [Header("Audio")]
    [SerializeField] private AudioSource solarLoop;
    [SerializeField] private AudioSource solarDone;

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
        CheckIfUsingEnergy();
    }

    public void StartPushBox()
    {
        usingEnergy = true;
    }

    public void StopPushingBox()
    {
        usingEnergy = false;
    }


    private void CheckIfUsingEnergy()
    {
        if (usingEnergy)
        {
            if (sunEnergy > 0) sunEnergy -= decreaseValue * Time.deltaTime;
        }
    }

    private void InSunlight()
    {
        if (sunEnergy < 100)
        {
            if (!solarLoop.isPlaying) solarLoop.Play();
            sunEnergy += increaseValue * Time.deltaTime;
        }
        if (sunEnergy > 100)
        {
            solarLoop.Stop();
            solarDone.Play();
            sunEnergy = 100;
        }
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
