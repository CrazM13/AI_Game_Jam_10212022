using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIManager {

	private List<Vector3> registeredPOIs;

	public POIManager() {
		this.registeredPOIs = new List<Vector3>();
		GroupPOI.Reset();
	}

	public void RegisterPOI(Vector3 poi) {
		registeredPOIs.Add(poi);
	}

	public Vector3 GetRandomPOI() {
		int index = Random.Range(0, registeredPOIs.Count);
		return registeredPOIs[index];
	}

	public int GetGroupID() {
		return GroupPOI.GetFollowID();
	}

	public Vector3 GetFollowPOI(Transform target, int groupID, float distance = 1) {
		return target.TransformPoint(distance * GroupPOI.GetPosition(groupID));
	}

}
