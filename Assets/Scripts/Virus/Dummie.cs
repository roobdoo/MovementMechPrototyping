using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummie : MonoBehaviour
{
    private GameObject canvas;
    private GameObject virusObject;
    private GameObject uploadingBackground;
    private Image uploadingImage;

    private bool virusUploaded;
    private float virusTimer;

    private void Awake()
    {
        canvas = this.transform.GetChild(0).gameObject;
        virusObject = canvas.transform.GetChild(0).gameObject;
        uploadingBackground = virusObject.transform.GetChild(0).gameObject;
        uploadingImage = uploadingBackground.transform.GetChild(0).GetComponent<Image>();
    }

    private float tempSecond;
    private void Update()
    {
        tempSecond += Time.deltaTime;
        if (tempSecond > 0)
        {
            virusTimer++;
            tempSecond = 0;
        }    

        if (virusUploaded)
            return;

        Debug.Log(virusTimer);

        if (virusTimer > 500f)
        {
            Debug.Log("decrease upload");
            float uploadNumb = uploadingImage.fillAmount;
            float newFillAmount = Mathf.Lerp(uploadNumb, uploadNumb -= 10, Time.deltaTime);
            uploadingImage.fillAmount = newFillAmount;
        }

    }

    public void UpdateVirus(float amount)
    {
        virusTimer = 0;
        virusObject.SetActive(true);
        amount = amount / 100;
        uploadingImage.fillAmount += amount;
        if (uploadingImage.fillAmount == 1)
            virusUploaded = true;
    }
}
