using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected void OnTriggerEnter(Collider c) {
		if (c.gameObject.layer == LayerMask.NameToLayer ("Chicken")) {
			// Destroy (c.gameObject);
			GameUI.Instance.Stop();
			GameUI.Instance.LosePopup();
		}
	}
}
