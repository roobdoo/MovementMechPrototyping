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

        if (currentAmmo > 70)
        {
            currentSpeedBoost = 4;
            pms.accelerationSpeed = 4000000;
            //pms.maxSpeed = 4000;
            speedBoostIndicator.text = "Speed Boost: " + currentSpeedBoost.ToString() + "x";
            speedBoostIndicator.color = Color.green;
        }
        else if (currentAmmo > 60 && currentAmmo < 70)
        {
            currentSpeedBoost = 3;
            pms.accelerationSpeed = 3000000;
            //pms.maxSpeed = 3000;
            speedBoostIndicator.text = "Speed Boost: " + currentSpeedBoost.ToString() + "x";
            speedBoostIndicator.color = Color.green;
        }
        else if (currentAmmo > 30 && currentAmmo < 60)
        {
            currentSpeedBoost = 2;
            pms.accelerationSpeed = 1000000;
            //pms.maxSpeed = 2000;
            speedBoostIndicator.text = "Speed Boost: " + currentSpeedBoost.ToString() + "x";
            speedBoostIndicator.color = Color.yellow;
        }
        else if (currentAmmo < 30)
        {
            currentSpeedBoost = 1;
            pms.accelerationSpeed = 500000;
            //pms.maxSpeed = 1000;
            speedBoostIndicator.text = "Speed Boost: " + currentSpeedBoost.ToString() + "x";
            speedBoostIndicator.color = Color.red;
        }
    }
}
