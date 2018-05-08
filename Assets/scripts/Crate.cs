using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected void OnTriggerEnter(Collider c) {
		if (c.gameObject.layer == LayerMask.NameToLayer ("Chicken")) {

			if (Attractor.Instance != null) {
				GameManager.Instance.GainPoints ();
				Attractor.Instance.DestroyChicken (c.transform);
			}
		}
	}
}
