using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CooldownEffect : MonoBehaviour {

	[SerializeField] private Image background;
	[SerializeField] private Image fill;

	public void SetFill(float percentage) {
		fill.fillAmount = percentage;

		if (percentage <= 0 || percentage >= 1) {
			if (background.gameObject.activeInHierarchy) background.gameObject.SetActive(false);
		} else {
			if (!background.gameObject.activeInHierarchy) background.gameObject.SetActive(true);
		}
	}

}
