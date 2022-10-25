using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GroupPOI {

	private const float PHI_SQR = 2.6180339887498948482045868343655f;

	private static int totalFollowing = 0;

	public static int GetFollowID() {
		int retID = totalFollowing;
		totalFollowing++;
		return retID;
	}

	public static void Reset() {
		totalFollowing = 0;
	}

	public static Vector3 GetPosition(int index) {
		int boundryPointCount = Mathf.RoundToInt(Mathf.Sqrt(totalFollowing));
		float distance = GetDistance(index, totalFollowing, boundryPointCount);
		float theta = (2 * Mathf.PI * index) / PHI_SQR;

		Vector3 newPosition = new Vector3(distance * Mathf.Cos(theta), 0, distance * Mathf.Sin(theta));
		return newPosition;
	}

	private static float GetDistance(int index, int pointCount, int boundryPointCount) {
		if (index > pointCount - boundryPointCount) {
			return 1;
		}
		if (index < 1) {
			return 0;
		} else {
			return Mathf.Sqrt(index - 0.5f) / Mathf.Sqrt(pointCount - (boundryPointCount + 1) / 2f);
		}
	}

}
