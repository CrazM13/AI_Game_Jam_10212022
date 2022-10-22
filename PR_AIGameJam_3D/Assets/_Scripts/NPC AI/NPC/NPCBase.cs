using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCBase : MonoBehaviour {

	[SerializeField] protected NPCSettings settings;
	[SerializeField] private AlertDisplayManager alertDisplay;

	private float movementSpeedModifier = 1;

	void Start() {
		ServiceLocator.NPCManager.RegisterNPC(this);
	}

	void Update() {
		UpdateMovement();
		OnNPCBehaviour();
	}

	public abstract void OnScheduledMove();

	#region Behaviour

	protected abstract void OnNPCBehaviour();

	private AlertStates alertState = AlertStates.NONE;
	public AlertStates GetAlertState() => alertState;

	protected bool InteractWithNPC(NPCBase npc) {
		if (alertState == AlertStates.ALERTED) {
			return false;
		} else {
			walkTo = npc.transform.position + npc.transform.forward;
			return true;
		}
	}

	protected void MoveToNewPOI() {
		targetPos = ServiceLocator.POIManager.GetRandomPOI();
	}

	public void OnInteractionEvent(InteractionAIEvent @event) {
		if (Vector3.Distance(@event.eventPosition, transform.position) < settings.maxNoticeDistance) {
			float chance = Mathf.Max(@event.alertChance, settings.minNoticeChance);
			if (alertState == AlertStates.QUESTIONING) chance += settings.questioningChanceIncrease;

			if (Random.value < chance) {
				alertState = AlertStates.ALERTED;
				alertDisplay.StartAlert();
			} else {
				alertState = AlertStates.QUESTIONING;
				alertDisplay.StartQuestioning();
			}

			AfterInteractionEvent(@event);
		}
	}

	protected abstract void AfterInteractionEvent(InteractionAIEvent @event);
	#endregion

	#region Pathfinding
	private Vector3? targetPos = null;
	private Vector3? walkTo = null;

	public bool IsPathing => walkTo.HasValue || targetPos.HasValue;

	private void UpdatePath() {
		if (targetPos.HasValue) {
			NavMeshPath path = new NavMeshPath();
			NavMesh.CalculatePath(transform.position, targetPos.Value, -1, path);

			if (path.corners.Length > 1) walkTo = path.corners[1];
			else walkTo = null;
		}
	}
	
	private void UpdateMovement() {
		UpdatePath();
		if (walkTo.HasValue) {
			Vector3 movementDirection = (walkTo.Value - transform.position).normalized;
			transform.position = Vector3.MoveTowards(transform.position, walkTo.Value, settings.movementSpeed * movementSpeedModifier * Time.deltaTime);
			transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);

			if (Vector3.Distance(transform.position, walkTo.Value) < settings.distanceToStop) {
				walkTo = null;

				if (targetPos.HasValue && Vector3.Distance(transform.position, targetPos.Value) < settings.distanceToStop) {
					targetPos = null;
				}
			}
		}
	}

	public void PathTo(Vector3 position) {
		targetPos = position;
	}

	public void SetSpeedModifier(float speedModifier) {
		this.movementSpeedModifier = speedModifier;
	}

	#endregion

}
