using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGhosthunter : NPCBase {
	public override void OnScheduledMove() {
		if (GetAlertState() == AlertStates.NONE) MoveToNewPOI();
		else if (!IsPathing) PathTo(ServiceLocator.Player.transform.position);
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

		AlertStates currentState = GetAlertState();
		if (currentState != AlertStates.ALERTED) PathTo(@event.eventPosition);
	}

	protected override void OnNPCBehaviour() {
		if (GetAlertState() == AlertStates.ALERTED) {
			float distanceToPlayer = Vector3.Distance(transform.position, ServiceLocator.Player.transform.position);
			bool canSeePlayer = distanceToPlayer < settings.maxNoticeDistance;
			if (distanceToPlayer < 1) {
				ServiceLocator.SceneManager.LoadSceneByName("Lose Scene", 2f);
			} else if (canSeePlayer) {
				PathTo(ServiceLocator.Player.transform.position);
			}
		}
	}

	protected override void OnInit() { /*MT*/ }
}
