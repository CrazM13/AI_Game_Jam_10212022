using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Settings", menuName = "NPC/Settings", order = 0)]
public class NPCSettings : ScriptableObject {

	public float movementSpeed = 1;
	public float distanceToStop = 1;
	public float maxNoticeDistance = 5;
	public float minNoticeChance = 0.3f;
	public float questioningChanceIncrease = 0.1f;
	public float calmRate = 0.1f;
	public bool canDie = false;

}
