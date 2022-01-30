using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWaterEnergy : MonoBehaviour
{
    private PlayerManager manager;

    [Range(0, 100)] public float waterEnergy;
    [SerializeField] private float suckTime;
    [SerializeField] private float decreaseValue;

    public bool waterSucc;
    public bool canWater;

    private Waterable currentWaterable;

    [Header("UI")]
    [SerializeField] private Image fillImage;

    [Header("Audio")]
    [SerializeField] private AudioSource waterCharge;
    [SerializeField] private AudioSource waterDone;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        fillImage = GameObject.Find("WaterEnergyFront").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.pickup.isHolding && Input.GetKeyDown(KeyCode.E) && CheckForWaterInRange() && !waterSucc && waterEnergy < 100)
        {
            StartCoroutine(WaterSucc());
        }
        if (!manager.pickup.isHolding && Input.GetKeyDown(KeyCode.E) && canWater)
        {
            Watering();
        }

        canWater = (CheckForWaterablesInRange() && waterEnergy > 0);

        UpdateUI();
    }

    private void UpdateUI()
    {
        fillImage.fillAmount = waterEnergy / 100;
    }

    private Water CheckForWaterInRange()
    {
        Water currentWater = null;

        if (manager.pickup.pickupCollider.water.Count > 0) currentWater = manager.pickup.pickupCollider.water[0];

        return currentWater;
    }

    private Waterable CheckForWaterablesInRange()
    {
        Waterable currentWable = null;

        if (manager.pickup.pickupCollider.waterables.Count > 0)
        {
            currentWable = manager.pickup.pickupCollider.waterables[0];
            currentWaterable = currentWable;
        }

        return currentWable;
    }

    private void Watering()
    {
        waterEnergy -= currentWaterable.waterValue;
        currentWaterable.GrowMe();
        manager.pickup.pickupCollider.waterables.Remove(currentWaterable);
        currentWaterable = null;
    }

    private IEnumerator WaterSucc()
    {
        waterSucc = true;
        manager.StartCoroutine(manager.SuckWater(suckTime));
        //yield return new WaitForSeconds(suckTime);
        float dividedValue = suckTime / 4;
        for (float i = 0; i <= suckTime; i++)
        {
            float addValueTotal = 100 - waterEnergy;
            float addValue = addValueTotal / suckTime;
            waterEnergy += addValue;
            waterCharge.Play();
            yield return new WaitForSeconds(dividedValue);
            //yield return new WaitForEndOfFrame();
        }
        waterDone.Play();
        waterEnergy = 100;
        waterSucc = false;
    }
}
