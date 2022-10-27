using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyManager : MonoBehaviour {

	[SerializeField] private new AudioSource audio;
	private float spookyness = 27;

	private float maxVolume = 1;

	private void Start() {
		ServiceLocator.AudioManager.PlayGlobal("Music", "Spoopy", true);
		maxVolume = audio.volume;
	}

	void Update() {
		spookyness -= Time.deltaTime;
		audio.volume = Mathf.Clamp(spookyness / 20f, maxVolume / 4f, maxVolume);
	}

	public void AddSpook() {
		spookyness += 20;
	}
}
