using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCBase : MonoBehaviour {

	[SerializeField] protected NPCSettings settings;
	[SerializeField] private AlertDisplayManager alertDisplay;
	[SerializeField] private Transform livingModelTransform;
	[SerializeField] private Transform ghostModelTransform;

	private float movementSpeedModifier = 1;
	public string ID { get; private set; }

	void Start() {
		ID = System.Guid.NewGuid().ToString();
		ServiceLocator.NPCManager.RegisterNPC(this);
		if (ghostModelTransform) ghostModelTransform.gameObject.SetActive(false);

		OnInit();
	}

	void Update() {
		UpdateMovement();
		OnNPCBehaviour();
	}

	public abstract void OnScheduledMove();

	public NPCSettings GetSettings() => settings;

	#region Behaviour

	protected abstract void OnInit();
	protected abstract void OnNPCBehaviour();

	private AlertStates alertState = AlertStates.NONE;
	public AlertStates GetAlertState() => alertState;
	public void SetAlertState(AlertStates state) {
		alertState = state;
		if (alertState == AlertStates.QUESTIONING) alertDisplay.StartQuestioning();
		else if (alertState == AlertStates.ALERTED) alertDisplay.StartAlert();
		else alertDisplay.HideAll();
	}

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
		if (alertState == AlertStates.DEAD) return;

		if (Vector3.Distance(@event.eventPosition, transform.position) < settings.maxNoticeDistance) {
			float chance = Mathf.Max(@event.alertChance, settings.minNoticeChance);
			if (alertState == AlertStates.QUESTIONING) chance += settings.questioningChanceIncrease;

			if (alertState == AlertStates.ALERTED) {
				if (settings.canDie && Random.value < chance) {
					alertState = AlertStates.DEAD;
					alertDisplay.HideAll();
					LeaveBody();
				}
			} else {
				if (Random.value < chance) {
					alertState = AlertStates.ALERTED;
					alertDisplay.StartAlert();
				} else {
					alertState = AlertStates.QUESTIONING;
					alertDisplay.StartQuestioning();
				}
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
		NavMesh.SamplePosition(position, out NavMeshHit hit, 1, -1);
		if (hit.hit) {
			targetPos = hit.position;
		}
	}

	public void SetSpeedModifier(float speedModifier) {
		this.movementSpeedModifier = speedModifier;
	}

	#endregion

	#region Visuals
	private void LeaveBody() {
		livingModelTransform.SetParent(null);
		Destroy(livingModelTransform.gameObject, 10);
		if (ghostModelTransform) ghostModelTransform.gameObject.SetActive(true);
	}
	#endregion

}
