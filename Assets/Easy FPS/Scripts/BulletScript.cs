using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000000;
	RaycastHit hit;
	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;
	[Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
	public float floatInfrontOfWall;
	[Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
	public GameObject bloodEffect;
	[Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
	public LayerMask ignoreLayer;

	/*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/
	void Update () {

		if(Physics.Raycast(transform.position, transform.forward,out hit, maxDistance, ~ignoreLayer)){
			if(decalHitWall){
				if(hit.transform.tag == "LevelPart"){
					Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
					Destroy(gameObject);
				}
				if(hit.transform.CompareTag("Dummie"))
                {
					GameObject dummie = hit.transform.gameObject;
					Dummie dummieScript = dummie.GetComponent<Dummie>();
					if (dummieScript != null)
					{
                        float amount = Random.Range(10, 30);
                        dummieScript.UpdateVirus(amount);
                    }
					Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
					Destroy(gameObject);
				}
				if (hit.transform.CompareTag("Terminal"))
				{
					GameObject terminal = hit.transform.gameObject;
					terminal.GetComponent<MeshRenderer>().material.color = Color.grey;
					terminal.tag = "Untagged";
                    GameObject dummie = hit.transform.parent.gameObject;
                    DummieV2 dummieScript = dummie.GetComponent<DummieV2>();
                    if (dummieScript != null)
                    {
                        dummieScript.numbOfDisabledTerminals += 1;
                        dummieScript.UpdateVirus();
                    }
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(gameObject);
                }
				if (hit.transform.CompareTag("Terminal2"))
				{
					Debug.Log("hit terminal 2");
                    GameObject terminal = hit.transform.gameObject;
                    terminal.GetComponent<MeshRenderer>().material.color = Color.grey;
                    GameObject dummie = terminal.transform.parent.gameObject;
                    Dummie dummieScript = dummie.GetComponent<Dummie>();
                    if (dummieScript != null)
                    {
                        float amount = Random.Range(10, 30);
                        dummieScript.UpdateVirus(amount);
                    }
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(gameObject);
                }
			}		
			Destroy(gameObject);
		}
		Destroy(gameObject, 0.1f);
	}

}
