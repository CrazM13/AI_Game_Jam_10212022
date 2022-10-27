using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

	#region Constants
	private const string MOVING_KEY = "Moving";
	#endregion

	[SerializeField] private Animator animator;

	void Update() {
		bool isMoving = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;

		animator.SetBool(MOVING_KEY, isMoving);
	}
}
