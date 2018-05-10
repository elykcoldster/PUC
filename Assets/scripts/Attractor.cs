using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attractor : Singleton<Attractor> {

	protected List<Transform> chickens;

	// Update is called once per frame
	void Update () {
		UpdatePosition ();
		UpdateChickens ();
	}

	protected void UpdatePosition() {
		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			int layerMask = 1 << LayerMask.NameToLayer ("Ground");

			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, layerMask)) {
				transform.position = hit.point + (Camera.main.transform.position - hit.point).normalized * 0.5f;
			}
		} else {
			ResetChickens ();
			Destroy (gameObject);
		}
	}

	protected void UpdateChickens() {
		if (chickens == null) {
			return;
		}

		foreach (Transform c in chickens) {
			c.position = transform.position;
			c.GetComponent<ChickenMovement>().SetState(ChickenState.Chaos);
		}
	}

	protected void ResetChickens() {
		if (chickens == null) {
			return;
		}
		/*
		foreach (Transform c in chickens) {
			c.GetComponent<NavMeshAgent> ().enabled = true;
			c.GetComponent<ChickenMovement> ().InitializeMovement ();
		}*/
	}

	public void AddChicken(Transform c) {
		if (this.chickens == null) {
			this.chickens = new List<Transform> ();
		}
		c.GetComponent<NavMeshAgent> ().enabled = false;
		this.chickens.Add (c);
	}

	public void DestroyChicken(Transform c) {
		if (this.chickens == null) {
			return;
		}

		this.chickens.Remove (c);
		Destroy (c.gameObject);
	}
}
