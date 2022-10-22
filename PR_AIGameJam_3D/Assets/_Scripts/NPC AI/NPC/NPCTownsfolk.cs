using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTownsfolk : NPCBase {

	public override void OnScheduledMove() {
		if (!IsPathing) MoveToNewPOI();
	}

	protected override void OnNPCBehaviour() {
		if (GetAlertState() == AlertStates.ALERTED) {
			if (!IsPathing) MoveToNewPOI();
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

}
