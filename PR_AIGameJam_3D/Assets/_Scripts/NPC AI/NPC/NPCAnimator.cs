using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour {

	#region Constants
	private const string DEATH_KEY = "Death";
	private const string ALERTED_KEY = "Alerted";
	private const string MOVING_KEY = "Moving";
	#endregion

	[SerializeField] private NPCBase npc;
	[SerializeField] private Animator animator;

	[SerializeField] private bool canDie;

	private AlertStates lastState = AlertStates.NONE;

	void Update() {
		if (!npc || !animator) return;

		bool isMoving = npc.IsPathing;

		AlertStates currentState = npc.GetAlertState();

		bool isAlerted = currentState == AlertStates.ALERTED;
		bool isDead = currentState == AlertStates.DEAD && lastState != AlertStates.DEAD;

		lastState = currentState;

		animator.SetBool(MOVING_KEY, isMoving);
		animator.SetBool(ALERTED_KEY, isAlerted);
		if (canDie && isDead) animator.SetTrigger(DEATH_KEY);

	}
}
