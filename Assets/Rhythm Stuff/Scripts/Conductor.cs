using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Conductor : MonoBehaviour
{
    // Conductor adapted to FPS variables:
    private PlayerMovementScript pms;

    [SerializeField] private Image stationaryRhythmIndicator;
    [SerializeField] private Image movingRhythmIndicator;
    private RectTransform movingIndicatorTransform;
    private Vector2 startSize;
    private Vector2 finishSize;

    private Color stationaryNeutralColour;
    private Color movingNeutralColour;
    [SerializeField] private Color onTime;
    [SerializeField] private Color offTime;

    private float timer = 0;
    public float Timer
    { get { return timer; } set { timer = value; OnTimerChange(value); } }
    [SerializeField] private float secPerHit;

    //shooting variables

    private bool isPressed;
    [SerializeField] private bool isMouseButton;
    [SerializeField] private float minPress;
    [SerializeField] private float maxPress;

    [SerializeField] private TextMeshProUGUI speedBoostIndicator;
    private int currentSpeedBoost;
    [SerializeField] private int maxSpeedBoost;

    void Start()
    {
        pms = GetComponent<PlayerMovementScript>();

        movingIndicatorTransform = movingRhythmIndicator.GetComponent<RectTransform>();

        startSize = movingIndicatorTransform.sizeDelta;
        finishSize = stationaryRhythmIndicator.GetComponent<RectTransform>().sizeDelta;

        stationaryNeutralColour = stationaryRhythmIndicator.color;
        movingNeutralColour = movingRhythmIndicator.color;
    }

    private float tempSecond;
    void Update()
    {
        Debug.Log(pms.currentSpeed);

        tempSecond += Time.deltaTime;
        
        if (tempSecond > 0)
        {
            Timer += tempSecond;
            tempSecond = 0;
        }

        if (isMouseButton && Input.GetMouseButtonDown(0))
        {
            CheckPressedRange();
            return;
        }
        
        if (!isMouseButton && Input.GetKeyDown(KeyCode.Space))
        {
            CheckPressedRange();
            return;
        }
    }

    private void OnTimerChange(float value)
    {
        if (value >= secPerHit)
        {
            Timer = 0;
            movingIndicatorTransform.sizeDelta = startSize;
            isPressed = false;
            stationaryRhythmIndicator.color = stationaryNeutralColour;
            movingRhythmIndicator.color = movingNeutralColour;
            return;
        }

        float t = value / secPerHit;
        t = Mathf.Clamp01(t);
        Vector2 changeSize = Vector2.Lerp(startSize, finishSize, t);
        movingIndicatorTransform.sizeDelta = changeSize;
    }

    private void CheckPressedRange()
    {
        if (isPressed)
            return;

        if (Timer > minPress && Timer < maxPress)
            EvaluateSpeedBoost(true);
        else
            EvaluateSpeedBoost(false);

            isPressed = true;
    }

    private void EvaluateSpeedBoost(bool value)
    {
        if (value)
        {
            stationaryRhythmIndicator.color = onTime;

            if (currentSpeedBoost < maxSpeedBoost)
            {
                currentSpeedBoost++;
                pms.accelerationSpeed += 1000000;
                pms.maxSpeed += 1000;
            }
        }
        else
        {
            movingRhythmIndicator.color = offTime;

            if (currentSpeedBoost > 1)
            {
                currentSpeedBoost--;
                pms.accelerationSpeed -= 1000000;
                pms.maxSpeed -= 1000;
            }
        }

        speedBoostIndicator.text = "Speed Boost: " + currentSpeedBoost.ToString() + "x";
    }
}
