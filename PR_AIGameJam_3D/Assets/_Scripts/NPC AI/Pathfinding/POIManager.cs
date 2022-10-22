using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIManager {

	private List<Vector3> registeredPOIs;

	public POIManager() {
		this.registeredPOIs = new List<Vector3>();
	}

	public void RegisterPOI(Vector3 poi) {
		registeredPOIs.Add(poi);
	}

	public Vector3 GetRandomPOI() {
		int index = Random.Range(0, registeredPOIs.Count);
		return registeredPOIs[index];
	}

}
