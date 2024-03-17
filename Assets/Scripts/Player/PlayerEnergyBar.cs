using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBar : MonoBehaviour
{
    private float energy;
    private float lerpTimer;
    public float maxEnergy = 100f;
    public float chipSpeed = 2f;
    public Image frontEnergyBar;
    public Image backEnergyBar;

    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        energy = Mathf.Clamp(energy, 0, maxEnergy);
        if (Input.GetKeyDown(KeyCode.G))
        {
            ConsumeEnergy(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            RestoreEnergy(Random.Range(5, 10));
        }
        UpdateEnergyUI();
    }

    public void UpdateEnergyUI()
    {
        float fillF = frontEnergyBar.fillAmount;
        float fillB = backEnergyBar.fillAmount;
        float eFraction = energy / maxEnergy;

        if (fillB > eFraction)
        {
            frontEnergyBar.fillAmount = eFraction;
            backEnergyBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backEnergyBar.fillAmount = Mathf.Lerp(fillB, eFraction, percentComplete);
        }
        if (fillF < eFraction)
        {
            backEnergyBar.color = Color.yellow;
            backEnergyBar.fillAmount = eFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontEnergyBar.fillAmount = Mathf.Lerp(fillF, backEnergyBar.fillAmount, percentComplete);
        }
    }

    public void ConsumeEnergy(float amount)
    {
        energy -= amount;
        lerpTimer = 0f;
    }

    public void RestoreEnergy(float amount)
    {
        energy += amount;
        lerpTimer = 0f;
    }
}