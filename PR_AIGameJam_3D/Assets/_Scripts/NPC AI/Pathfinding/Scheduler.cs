using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler : MonoBehaviour {

	[SerializeField] private float timeInterval;

	private float timeUntilChange = 0;

	private void Update() {
		timeUntilChange -= Time.deltaTime;
		if (timeUntilChange <= 0) {
			timeUntilChange = timeInterval;
			ServiceLocator.NPCManager.OnTimeChange();
			if (ServiceLocator.NPCManager.IsComplete()) {
				ServiceLocator.SceneManager.LoadSceneByName("Win Scene");
			}
		}
	}

}
