using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingPOI : MonoBehaviour {
	
	void Start() {
		ServiceLocator.POIManager.RegisterPOI(transform.position);
	}

}
