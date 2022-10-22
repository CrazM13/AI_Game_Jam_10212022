using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
	public Transform cam;
	public float playerActivateDistance;
	public float alertChance;
	bool active = false;


	public void Update() {
		RaycastHit hit;
		active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance);

		if (Input.GetKeyDown(KeyCode.F) && active == true) {
			ServiceLocator.NPCManager.OnInteractionEvent(new InteractionAIEvent() {
				eventPosition = transform.position,
				alertChance = alertChance // Put the actual chance here
			});

			if (hit.transform.GetComponentInChildren<Animator>() != null) {
				hit.transform.GetComponentInChildren<Animator>().SetTrigger("Activate");
			}

		}

	}

}

