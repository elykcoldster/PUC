using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ChickenState {
	Default,
	Chaos
}

[RequireComponent(typeof(NavMeshAgent))]
public class ChickenMovement : MonoBehaviour {

	private NavMeshAgent agent;
	protected ChickenState state;

	public ChickenState State {
		get { return this.state; }
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		InitializeMovement ();
	}

	protected void Update() {
		if (GameController.Instance.gameState != GameState.Playing && agent.enabled) {
			agent.ResetPath ();
		} else {
			InitializeMovement ();
		}
	}

	protected void OnCollisionEnter(Collision c) {
		if (c.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			agent.enabled = true;
			InitializeMovement ();
			SetState (ChickenState.Default);
		}
	}

	protected void OnMouseDown() {

		if (GameController.Instance.gameState != GameState.Playing) {
			return;
		}

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

	public void CancelNavigation(bool trigger) {
		agent.enabled = false;
		GetComponent<Collider> ().isTrigger = trigger;
	}

	public void SetState(ChickenState cs) {
		this.state = cs;
	}
}
