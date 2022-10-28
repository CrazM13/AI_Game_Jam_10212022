using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	[SerializeField, Range(0, 1)] private float alertChance;
	[SerializeField] private float cooldownDuration;
	[SerializeField] private string interactionSound;

	private float timeUntilReady = 0;

	[SerializeField] private Animator animator;
	[SerializeField] private SelectionEffect selectionEffect;
	[SerializeField] private SelectionEffect rangeEffect;

	public string ID { get; set; }

	public bool IsReady => timeUntilReady <= 0;

	private void Start() {
		ID = System.Guid.NewGuid().ToString();

		ServiceLocator.CooldownManager.AddEffect(ID, transform.position);
	}

	void Update() {
		if (!IsReady) {
			timeUntilReady -= Time.deltaTime;
			if (IsReady) {
				selectionEffect.ShowReady(true);
				rangeEffect.ShowReady(true);
			}
		}

		ServiceLocator.CooldownManager.SetValue(ID, transform.position, timeUntilReady / cooldownDuration);
	}

	public void Interact() {
		if (!IsReady) return;

		ServiceLocator.NPCManager.OnInteractionEvent(new InteractionAIEvent() {
			eventPosition = transform.position,
			alertChance = alertChance // Put the actual chance here
		});

		timeUntilReady = cooldownDuration;
		selectionEffect.ShowReady(false);

		if (ServiceLocator.SpookyManager) ServiceLocator.SpookyManager.AddSpook();
		if (!string.IsNullOrEmpty(interactionSound)) {
			ServiceLocator.AudioManager.PlayLocal(transform.position, "SoundFX", interactionSound);
		}

		if (animator) animator.SetTrigger("Activate");
	}

	public void ShowSelectionEffect(bool shouldShow) {
		selectionEffect.Show(shouldShow);
		rangeEffect.Show(shouldShow);
	}

}
