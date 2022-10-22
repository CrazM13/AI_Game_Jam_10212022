using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour {
	// Readonly services
	public static SceneTransition @SceneManager { get; set; }
	public static NPCManager @NPCManager { get; set; }
	public static POIManager @POIManager { get; set; }

	// Singleton
	private static ServiceLocator instance;

	private void Awake() {
		if (instance != null && instance != this) {
			Destroy(this);
			return;
		}
		instance = this;
		LocateServices();
	}

	private void LocateServices() {
		@SceneManager = FindObjectOfType<SceneTransition>();
		@NPCManager = new NPCManager();
		@POIManager = new POIManager();
	}

	private void OnDestroy() {
		@SceneManager = null;
	}
}
