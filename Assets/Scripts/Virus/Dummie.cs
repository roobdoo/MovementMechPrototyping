using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dummie : MonoBehaviour
{
    private GameObject canvas;
    private GameObject virusObject;
    private GameObject uploadingBackground;
    private Image uploadingImage;
    private GameObject uploadingText;
    private GameObject followTrigger;

    private bool virusUploaded;
    private float virusTimer;

    [SerializeField] private GameObject lightningPrefab;
    private GameObject lightning;

    private bool isDeactivated;

    public GameObject caughtScreen;

    private void Awake()
    {
        canvas = this.transform.GetChild(0).gameObject;
        virusObject = canvas.transform.GetChild(0).gameObject;
        uploadingBackground = virusObject.transform.GetChild(0).gameObject;
        uploadingImage = uploadingBackground.transform.GetChild(0).GetComponent<Image>();
        uploadingText = uploadingImage.transform.GetChild(0).gameObject;
        followTrigger = this.transform.GetChild(1).gameObject;
        Debug.Log(uploadingText);
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

        if (isDeactivated)
        {
            followTrigger.GetComponent<FollowTrigger>().enabled = false;
        }
        
        if (Input.GetMouseButtonDown(1) && virusUploaded && !isDeactivated)
        {
            lightning = Instantiate(lightningPrefab, this.transform);
            lightning.transform.localScale = (this.transform.localScale / 2);
            lightning.GetComponent<ParticleSystem>().Play();
            uploadingText.GetComponent<TextMeshProUGUI>().text = "Deactivated";
            this.GetComponent<AudioSource>().enabled = true;
            isDeactivated = true;
        }

        if (virusUploaded)
            return;

        if (virusTimer > 200f)
        {
            float uploadNumb = uploadingImage.fillAmount;
            float newFillAmount = Mathf.Lerp(uploadNumb, uploadNumb -= 10, Time.deltaTime);
            uploadingImage.fillAmount = newFillAmount;
        }

        if (uploadingImage.fillAmount == 0)
            virusObject.SetActive(false);
    }

    public void UpdateVirus(float amount)
    {
        virusTimer = 0;
        virusObject.SetActive(true);
        amount = amount / 100;
        uploadingImage.fillAmount += amount;
        if (uploadingImage.fillAmount == 1)
        {
            virusUploaded = true;
            uploadingText.GetComponent<TextMeshProUGUI>().text = "Uploaded";
        }
    }
}
