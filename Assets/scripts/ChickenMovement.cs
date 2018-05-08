using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChickenMovement : MonoBehaviour {

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		InitializeMovement ();
	}

	protected void Update() {
		if (GameController.Instance.gameState != GameState.Playing) {
			agent.ResetPath ();
		} else {
			InitializeMovement ();
		}
	}

	protected void OnMouseDown() {
		RaycastHit hit;
		int layerMask = 1 << LayerMask.NameToLayer ("Ground");

		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask)) {
			// Create chicken attractor
			Attractor att = (Instantiate<GameObject>(GameManager.Instance.Attractor)).GetComponent<Attractor>();
			att.AddChicken (transform);
		}
	}

	public void InitializeMovement() {
		if (agent.enabled) {
			agent.SetDestination (GameManager.Instance.ChickenTarget.position);
		}
	}
}
