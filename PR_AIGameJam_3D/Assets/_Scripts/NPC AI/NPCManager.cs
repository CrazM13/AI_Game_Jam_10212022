using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager {

	private List<NPCBase> registeredNPCs;

	public NPCManager() {
		this.registeredNPCs = new List<NPCBase>();
	}

	public void RegisterNPC(NPCBase npc) {
		registeredNPCs.Add(npc);
	}

	public void OnInteractionEvent(InteractionAIEvent @event) {
		foreach (NPCBase npc in registeredNPCs) {
			npc.OnInteractionEvent(@event);
		}
	}

	public void OnTimeChange() {
		foreach (NPCBase npc in registeredNPCs) {
			npc.OnScheduledMove();
		}
	}

	public bool IsComplete() {
		foreach (NPCBase npc in registeredNPCs) {
			if (npc is NPCTownsfolk townsfolk) {
				if (townsfolk.GetAlertState() != AlertStates.ALERTED) return false;
			}
		}

		return true;
	}

}
