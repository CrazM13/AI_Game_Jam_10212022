using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUse : MonoBehaviour {

	[SerializeField] private float useDistance = 1;

	private Interactable lastInteractable;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		Interactable nearest = GetClosestInteractable();
		if (nearest != lastInteractable) {
			if (lastInteractable) lastInteractable.ShowSelectionEffect(false);
			if (nearest) nearest.ShowSelectionEffect(true);
			lastInteractable = nearest;
		}

		if (nearest && Input.GetKeyDown(KeyCode.E)) {
			nearest.Interact();
		}
	}

	private List<Interactable> GetNearbyInteractables() {
		List<Interactable> results = new List<Interactable>();
		Collider[] colliders = Physics.OverlapSphere(transform.position, useDistance, -1);

		foreach (Collider c in colliders) {
			Interactable interactable = c.GetComponent<Interactable>();
			if (interactable) results.Add(interactable);
		}

		return results;
	}

	private Interactable GetClosestInteractable() {
		List<Interactable> results = GetNearbyInteractables();

		Interactable nearestInteractable = null;
		float nearestDistance = float.MaxValue;
		foreach (Interactable i in results) {
			float newDistance = Vector3.Distance(transform.position, i.transform.position);

			if (newDistance < nearestDistance) {
				nearestInteractable = i;
				nearestDistance = newDistance;
			}
		}

		return nearestInteractable;
	}

}
