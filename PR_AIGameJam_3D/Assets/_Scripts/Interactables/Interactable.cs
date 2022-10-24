using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	[SerializeField, Range(0, 1)] private float alertChance;
	[SerializeField] private float cooldownDuration;

	private float timeUntilReady = 0;

	[SerializeField] private Animator animator;
	[SerializeField] private SelectionEffect selectionEffect;

	public bool IsReady => timeUntilReady <= 0;

	void Update() {
		if (!IsReady) {
			timeUntilReady -= Time.deltaTime;
			if (IsReady) selectionEffect.ShowReady(true);
		}
	}

	public void Interact() {
		if (!IsReady) return;

		ServiceLocator.NPCManager.OnInteractionEvent(new InteractionAIEvent() {
			eventPosition = transform.position,
			alertChance = alertChance // Put the actual chance here
		});

		timeUntilReady = cooldownDuration;
		selectionEffect.ShowReady(false);

		if (animator) animator.SetTrigger("Activate");
	}

	public void ShowSelectionEffect(bool shouldShow) {
		selectionEffect.Show(shouldShow);
	}

}