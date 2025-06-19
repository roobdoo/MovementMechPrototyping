using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrigger : MonoBehaviour
{
    private bool isChasing;
    private GameObject dummie;
    private GameObject player;

    private float moveSpeed = 5;

    [SerializeField] private float height;

    private void Awake()
    {
        dummie = this.gameObject.transform.parent.gameObject;
        height = dummie.transform.position.y;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("follow player");
            isChasing = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isChasing = false;
        }
    }

    private void Update()
    {
        if (!isChasing)
            return;

        if (Vector3.Distance(dummie.transform.position, player.transform.position) < 15f)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            player.GetComponent<PlayerMovementScript>().accelerationSpeed = 0;
            DummieV2 dum2 = dummie.GetComponent<DummieV2>();
            if (dum2 != null)
                dum2.caughtScreen.SetActive(true);
            Dummie dum = dummie.GetComponent<Dummie>();
            if (dum != null)
                dum.caughtScreen.SetActive(true);
            return;
        }

        Vector3 playerPos = player.transform.position;
        Vector3 targetPos = Vector3.Lerp(dummie.transform.position, playerPos, 0.5f * Time.deltaTime);
        targetPos.y = height;
        dummie.transform.position = targetPos;
    }
}
