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

	}

}
