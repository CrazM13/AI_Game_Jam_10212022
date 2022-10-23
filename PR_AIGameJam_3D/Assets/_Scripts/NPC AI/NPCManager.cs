using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager {

	private Dictionary<string, NPCBase> registeredNPCs;

	public NPCManager() {
		this.registeredNPCs = new Dictionary<string, NPCBase>();
	}

	public void RegisterNPC(NPCBase npc) {
		registeredNPCs.Add(npc.ID, npc);
	}

	public void OnInteractionEvent(InteractionAIEvent @event) {
		foreach (NPCBase npc in registeredNPCs.Values) {
			npc.OnInteractionEvent(@event);
		}
	}

	public void OnTimeChange() {
		foreach (NPCBase npc in registeredNPCs.Values) {
			npc.OnScheduledMove();
		}
	}

	public bool IsComplete() {
		foreach (NPCBase npc in registeredNPCs.Values) {
			if (npc is NPCTownsfolk townsfolk) {
				if (townsfolk.GetAlertState() != AlertStates.ALERTED) return false;
			}
		}

		return true;
	}

	public NPCBase Find(string id) {
		return registeredNPCs[id];
	}

	public NPCBase Find(System.Func<NPCBase, bool> condition) {
		foreach (NPCBase npc in registeredNPCs.Values) {
			if (condition.Invoke(npc)) {
				return npc;
			}
		}
		return null;
	}

	public NPCBase FindNearest(Vector3 position, System.Func<NPCBase, bool> condition, float maxDistance = float.MaxValue) {
		List<NPCBase> results = FindAll(condition);
		NPCBase nearestNPC = null;
		float nearestDistance = maxDistance;

		foreach (NPCBase npc in results) {
			float newDistance = Vector3.Distance(position, npc.transform.position);
			if (newDistance < nearestDistance) {
				nearestNPC = npc;
				nearestDistance = newDistance;
			}
		}

		return nearestNPC;
	}

	public List<NPCBase> FindAll(System.Func<NPCBase, bool> condition) {
		List<NPCBase> foundResults = new List<NPCBase>();

		foreach (NPCBase npc in registeredNPCs.Values) {
			if (condition.Invoke(npc)) {
				foundResults.Add(npc);
			}
		}

		return foundResults;
	}

}
