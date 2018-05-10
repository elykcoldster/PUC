using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Singleton<Crate> {
	
	protected void OnTriggerEnter(Collider c) {
		if (c.gameObject.layer == LayerMask.NameToLayer ("Chicken")) {

			if (Attractor.Instance != null) {
				GameManager.Instance.GainPoints ();
				Attractor.Instance.DestroyChicken (c.transform);
			} else {
				GameManager.Instance.GainPoints ();
				Destroy (c.gameObject);
			}
		}
	}
}
