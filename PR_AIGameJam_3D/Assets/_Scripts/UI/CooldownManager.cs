using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour {

	[SerializeField] private GameObject prefab;

	private Dictionary<string, CooldownEffect> effects = new Dictionary<string, CooldownEffect>();

	
	public void SetValue(string id, Vector3 worldPosition, float percentage) {
		if (effects.ContainsKey(id)) {
			effects[id].SetFill(percentage);
			effects[id].transform.position = ConvertToCanvas(worldPosition);
		} else {
			AddEffect(id, worldPosition);
		}

	}

	public void AddEffect(string id, Vector3 worldPosition) {

		Vector3 canvasPos = ConvertToCanvas(worldPosition);

		CooldownEffect effect = Instantiate(prefab, transform).GetComponent<CooldownEffect>();
		if (effect) {
			effect.transform.position = canvasPos;

			effects.Add(id, effect);
		}
	}

	private Vector3 ConvertToCanvas(Vector3 worldPosition) {
		return RectTransformUtility.WorldToScreenPoint(Camera.main, worldPosition + (Vector3.up * 2));
	}

}
