using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSpeedManager : MonoBehaviour
{
    private PlayerMovementScript pms;

    [SerializeField] private TextMeshProUGUI speedBoostIndicator;
    private int currentSpeedBoost;

    [SerializeField] private GunScript gunScript;

    private float currentAmmo;

    private void Awake()
    {
        pms = GetComponent<PlayerMovementScript>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            EvaluateSpeedBoost();
    }

    private void EvaluateSpeedBoost()
    {
        gunScript = GameObject.FindWithTag("Weapon").GetComponent<GunScript>();
        float currentAmmo = gunScript.bulletsInTheGun;

        if (currentAmmo > 64)
        {
            currentSpeedBoost = 5;
            pms.accelerationSpeed = 4000000;
            //pms.maxSpeed = 5000;
            speedBoostIndicator.text = "Speed Boost: " + currentSpeedBoost.ToString() + "x";
        }
        else if (currentAmmo > 48 && currentAmmo < 64)
        {
            currentSpeedBoost = 4;
            pms.accelerationSpeed = 3500000;
            //pms.maxSpeed = 4000;
            speedBoostIndicator.text = "Speed Boost: " + currentSpeedBoost.ToString() + "x";
        }
        else if (currentAmmo > 32 && currentAmmo < 48)
        {
            currentSpeedBoost = 3;
            pms.accelerationSpeed = 3000000;
            //pms.maxSpeed = 3000;
            speedBoostIndicator.text = "Speed Boost: " + currentSpeedBoost.ToString() + "x";
        }
        else if (currentAmmo > 16 && currentAmmo < 32)
        {
            currentSpeedBoost = 2;
            pms.accelerationSpeed = 1000000;
            //pms.maxSpeed = 2000;
            speedBoostIndicator.text = "Speed Boost: " + currentSpeedBoost.ToString() + "x";
        }
        else if (currentAmmo < 16)
        {
            currentSpeedBoost = 1;
            pms.accelerationSpeed = 500000;
            //pms.maxSpeed = 1000;
            speedBoostIndicator.text = "Speed Boost: " + currentSpeedBoost.ToString() + "x";
        }
    }
}
