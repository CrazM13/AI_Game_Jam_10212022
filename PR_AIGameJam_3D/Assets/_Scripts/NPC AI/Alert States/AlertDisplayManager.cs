using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertDisplayManager : MonoBehaviour {

	[SerializeField] private DisplayAlert questioning;
	[SerializeField] private DisplayAlert alert;

	
	public void StartQuestioning() {
		questioning.Display();
		alert.Hide();
	}

	public void StartAlert() {
		questioning.Hide();
		alert.Display();
	}

	public void HideAll() {
		questioning.Hide();
		alert.Hide();
	}

}
