using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour {
	// Readonly services
	public static SceneTransition @SceneManager { get; set; }
	public static AudioManager @AudioManager { get; set; }
	public static NPCManager @NPCManager { get; set; }
	public static POIManager @POIManager { get; set; }
	public static PlayerMovement Player { get; set; }
	public static CooldownManager @CooldownManager { get; set; }

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
		@AudioManager = FindObjectOfType<AudioManager>();
		@NPCManager = new NPCManager();
		@POIManager = new POIManager();
		Player = FindObjectOfType<PlayerMovement>();
		@CooldownManager = FindObjectOfType<CooldownManager>();
	}

	private void OnDestroy() {
		if (instance == this) {
			@SceneManager = null;
			@AudioManager = null;
			@NPCManager = null;
			@POIManager = null;
			Player = null;
			@CooldownManager = null;
		}
	}
}
