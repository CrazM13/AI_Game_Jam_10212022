using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTownsfolk : NPCBase {

	private string ghosthunterID = null;

	public override void OnScheduledMove() {
		AlertStates currentState = GetAlertState();

		bool canCalmDown = currentState == AlertStates.QUESTIONING || currentState == AlertStates.ALERTED;
		if (canCalmDown && ghosthunterID != null) {

			NPCBase ghosthunter = ServiceLocator.NPCManager.Find(ghosthunterID);

			if (ghosthunter) {
				float distanceToGhosthunter = Vector3.Distance(ghosthunter.transform.position, transform.position);

				if (distanceToGhosthunter < settings.maxNoticeDistance) {
					PathTo(ghosthunter.transform.position);
				} else MoveToNewPOI();
			} else MoveToNewPOI();

			AttemptCalmDown(currentState, ghosthunter);

		} else if (!IsPathing) {
			MoveToNewPOI();
		}
	}

	protected override void OnNPCBehaviour() {
		AlertStates currentState = GetAlertState();

		bool canCalmDown = currentState == AlertStates.QUESTIONING || currentState == AlertStates.ALERTED;
		if (!IsPathing && canCalmDown) {
			if (ghosthunterID != null) {
				NPCBase ghosthunter = ServiceLocator.NPCManager.Find(ghosthunterID);

				if (ghosthunter) {
					float distanceToGhosthunter = Vector3.Distance(ghosthunter.transform.position, transform.position);

					if (distanceToGhosthunter < settings.maxNoticeDistance) {
						if (distanceToGhosthunter > settings.distanceToStop) PathTo(ghosthunter.transform.position);
					} else {
						ghosthunterID = null;
						MoveToNewPOI();
					}
				} else MoveToNewPOI();
			} else {
				NPCBase ghosthunter = ServiceLocator.NPCManager.FindNearest(transform.position, (npc) => npc is NPCGhosthunter, settings.maxNoticeDistance);
				if (ghosthunter) {
					ghosthunterID = ghosthunter.ID;
				} else {
					MoveToNewPOI();
				}
			}
		}
	}

	protected override void AfterInteractionEvent(InteractionAIEvent @event) {
		switch (GetAlertState()) {
			case AlertStates.QUESTIONING:
				SetSpeedModifier(1.1f);
				break;
			case AlertStates.ALERTED:
				SetSpeedModifier(2f);
				break;
		}
		MoveToNewPOI();
	}

	private void AttemptCalmDown(AlertStates currentState, NPCBase ghosthunter) {
		float modifiedCalmRate = settings.calmRate;

		if (ghosthunter) {
			NPCSettings ghostHunterSettings = ghosthunter.GetSettings();
			if (Vector3.Distance(transform.position, ghosthunter.transform.position) < ghostHunterSettings.maxNoticeDistance) {
				modifiedCalmRate += ghostHunterSettings.calmRate;
			}
		}

		if (Random.value < modifiedCalmRate) {
			switch (currentState) {
				case AlertStates.QUESTIONING:
					SetAlertState(AlertStates.NONE);
					ghosthunterID = null;
					MoveToNewPOI();
					break;
				case AlertStates.ALERTED:
					SetAlertState(AlertStates.QUESTIONING);
					break;
			}
		}
	}

}
