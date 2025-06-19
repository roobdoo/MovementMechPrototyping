using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrigger : MonoBehaviour
{
    private bool isChasing;
    private GameObject dummie;
    private GameObject player;

    private float moveSpeed = 5;

    private void Awake()
    {
        dummie = this.gameObject.transform.parent.gameObject;
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
            return;

        Vector3 playerPos = player.transform.position;
        Vector3 targetPos = Vector3.Lerp(dummie.transform.position, playerPos, 0.5f * Time.deltaTime);
        targetPos.y = 9f;
        dummie.transform.position = targetPos;
    }
}
