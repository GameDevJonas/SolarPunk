using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWaterEnergy : MonoBehaviour
{
    private PlayerManager manager;

    [Range(0, 100)] public float waterEnergy;
    [SerializeField] private float suckTime;

    public bool waterSucc;

    [Header("UI")]
    [SerializeField] private Image fillImage;

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
            yield return new WaitForSeconds(dividedValue);
            //yield return new WaitForEndOfFrame();
        }
        waterEnergy = 100;
        waterSucc = false;
    }
}
