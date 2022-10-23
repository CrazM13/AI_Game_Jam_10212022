using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionEffect : MonoBehaviour {

	[SerializeField] private new MeshRenderer renderer;

	void Start() {
		renderer.material.SetInt("ShouldShow", 0);
		renderer.material.SetInt("IsReady", 1);
	}

	public void ShowReady(bool isReady) {
		renderer.material.SetInt("IsReady", isReady ? 1 : 0);
	}

	public void Show(bool shouldShow) {
		renderer.material.SetInt("ShouldShow", shouldShow ? 1 : 0);
	}

}
