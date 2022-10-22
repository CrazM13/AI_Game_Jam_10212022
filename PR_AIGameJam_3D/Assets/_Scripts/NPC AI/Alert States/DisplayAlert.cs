using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAlert : MonoBehaviour {

	[SerializeField] private float displaySpeed = 1;
	[SerializeField] private AnimationCurve displayCurve;

	private float displayTime = 0;

	private bool shouldDisplay = false;
	private bool isAnimating = false;

	void Start() {
		transform.localScale = Vector3.zero;
	}

	void Update() {
		if (isAnimating) {
			displayTime += (shouldDisplay ? 1 : -1) * Time.deltaTime * displaySpeed;

			if (shouldDisplay && displayTime > 1) {
				isAnimating = false;
				displayTime = 1;
			} else if (!shouldDisplay && displayTime < 0) {
				isAnimating = false;
				displayTime = 0;
			}

			transform.localScale = Vector3.one * displayCurve.Evaluate(displayTime);
		}
	}

	[ContextMenu("Display")]
	public void Display() {
		isAnimating = true;
		shouldDisplay = true;
	}

	[ContextMenu("Hide")]
	public void Hide() {
		isAnimating = true;
		shouldDisplay = false;
	}
}
