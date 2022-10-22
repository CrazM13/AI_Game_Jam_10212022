using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCBase : MonoBehaviour {

	private string id;

	void Start() {
		id = System.Guid.NewGuid().ToString();

		ServiceLocator.NPCManager.RegisterNPC(this);
	}

	void Update() {
		UpdateMovement();
	}

	#region Pathfinding
	private Vector3? targetPos = null;
	private Vector3? walkTo = null;

	private void UpdatePath() {
		if (targetPos.HasValue) {
			NavMeshPath path = new NavMeshPath();
			NavMesh.CalculatePath(transform.position, targetPos.Value, -1, path);

			if (path.corners.Length > 0) walkTo = path.corners[0];
			else walkTo = null;
		}
	}
	
	private void UpdateMovement() {
		UpdatePath();
		if (walkTo.HasValue) {
			transform.position = Vector3.MoveTowards(transform.position, walkTo.Value, Time.deltaTime);
		}
	}

	public void PathTo(Vector3 position) {
		targetPos = position;
	}

	#endregion

	#region Operators
	public override bool Equals(object other) {
		return (other is NPCBase npc) && (npc.id == id);
	}

	public override int GetHashCode() {
		return base.GetHashCode();
	}
	#endregion

}
