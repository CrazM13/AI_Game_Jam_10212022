using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTownsfolk : NPCBase {

	private NPCBase ghosthunter = null;
	private int groupID = 0;

	public override void OnScheduledMove() {
		AlertStates currentState = GetAlertState();

		bool canCalmDown = currentState == AlertStates.QUESTIONING || currentState == AlertStates.ALERTED;
		if (canCalmDown && ghosthunter) {
			if (!AttemptMoveToGhosthunter()) MoveToNewPOI();
			AttemptCalmDown(currentState, ghosthunter);
		} else if (!IsPathing) {
			MoveToNewPOI();
		}
	}

	protected override void OnNPCBehaviour() {
		AlertStates currentState = GetAlertState();

		bool canCalmDown = currentState == AlertStates.QUESTIONING || currentState == AlertStates.ALERTED;
		if (canCalmDown) {
			if (ghosthunter) {
				if (ghosthunter) {
					float distanceToGhosthunter = Vector3.Distance(ghosthunter.transform.position, transform.position);

					if (distanceToGhosthunter < settings.maxNoticeDistance) {
						if (distanceToGhosthunter > settings.distanceToStop) PathTo(ghosthunter.transform.position);
					} else {
						ghosthunter = null;
						MoveToNewPOI();
					}
				} else if (!IsPathing) MoveToNewPOI();
			} else {
				NPCBase ghosthunter = ServiceLocator.NPCManager.FindNearest(transform.position, (npc) => npc is NPCGhosthunter, settings.maxNoticeDistance);
				if (ghosthunter) {
					this.ghosthunter = ghosthunter;
				} else {
					if (!IsPathing) MoveToNewPOI();
				}
			}
		} else if (currentState == AlertStates.DEAD) {
			ChasePlayer();
		}
	}

	private void ChasePlayer() {
		Transform player = ServiceLocator.Player.transform;

		if (player) {
			Vector3 playerPosition = ServiceLocator.POIManager.GetFollowPOI(player, groupID, 2);

			float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
			if (distanceToPlayer > settings.distanceToStop) PathTo(playerPosition);
		}
	}

	protected override void AfterInteractionEvent(InteractionAIEvent @event) {
		switch (GetAlertState()) {
			case AlertStates.QUESTIONING:
				SetSpeedModifier(1.1f);
				ServiceLocator.AudioManager.PlayRandomLocal(transform.position, "Questioning");
				break;
			case AlertStates.ALERTED:
				SetSpeedModifier(2f);
				ServiceLocator.AudioManager.PlayRandomLocal(transform.position, "Alerted");
				break;
			case AlertStates.DEAD:
				ServiceLocator.AudioManager.PlayRandomLocal(transform.position, "Dead");
				if (ServiceLocator.NPCManager.IsComplete()) {
					ServiceLocator.SceneManager.LoadSceneByName("Win Scene", 10f);
				}
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
					ghosthunter = null;
					MoveToNewPOI();
					ServiceLocator.AudioManager.PlayRandomLocal(transform.position, "Idle");
					break;
				case AlertStates.ALERTED:
					SetAlertState(AlertStates.QUESTIONING);
					break;
			}
		}
	}

	protected override void OnInit() {
		groupID = ServiceLocator.POIManager.GetGroupID();
	}

	private bool AttemptMoveToGhosthunter() {
		if (ghosthunter) {
			Vector3 ghosthunterPos = ServiceLocator.POIManager.GetFollowPOI(ghosthunter.transform, groupID, 3);
			float distanceToGhosthunter = Vector3.Distance(ghosthunterPos, transform.position);

			if (distanceToGhosthunter < settings.maxNoticeDistance) {
				PathTo(ghosthunterPos);
				return true;
			} else return false;
		} else return false;
	}
}
