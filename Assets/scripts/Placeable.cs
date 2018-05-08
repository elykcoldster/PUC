using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlacementState {
	Placing,
	Collision,
	Placed
}

public class Placeable : MonoBehaviour {

	[SerializeField]
	protected int cost = 20;

	protected PlacementState state;

	protected Transform marker;
	protected Material mat;
	protected Collider coll;
	protected NavMeshObstacle obs;

	protected Color matColor, invalidColor = new Color(1f, 0f, 0f, 1f);

	protected List<Collider> collisions;

	// Use this for initialization
	protected virtual void Awake () {

		gameObject.layer = LayerMask.NameToLayer ("Placeable");

		state = PlacementState.Placing;

		coll = GetComponent<Collider> ();
		mat = GetComponent<MeshRenderer> ().material;
		matColor = mat.color;
		obs = GetComponent<NavMeshObstacle> ();

		SetObjectCollision (false);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		CheckPlacementConfirm ();
		FollowMarker ();
		CheckRotation ();
		CheckCancel ();
		CorrectYPosition ();
	}

	protected virtual void LateUpdate() {
		UpdateCollisions ();
		UpdatePlaceableColor ();
	}

	protected void CheckCancel() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Destroy (GridPlacement.Instance.gameObject);
			Destroy (gameObject);
		}
	}

	protected void CheckPlacementConfirm() {

		if (state == PlacementState.Placed) {
			return;
		}

		if (Input.GetMouseButtonDown (0) && state != PlacementState.Collision) {

			if (!GameManager.Instance.SpendPoints (this.cost)) {
				return;
			}

			this.state = PlacementState.Placed;
			SetObjectCollision (true);

			GridPlacement.Instance.PlacementComplete ();
			// Destroy (GridPlacement.Instance.gameObject);
		}
	}

	protected void CheckRotation() {
		if (state == PlacementState.Placed) {
			return;
		}

		if (Input.GetMouseButtonDown (1)) {
			transform.Rotate (new Vector3 (0f, 90f, 0f));
		}
	}

	protected void CorrectYPosition () {
		transform.position = new Vector3 (transform.position.x, 0f, transform.position.z);
	}

	protected void UpdateCollisions() {

		if (state == PlacementState.Placed) {
			return;
		}

		if (collisions != null && collisions.Count > 0) {
			state = PlacementState.Collision;
		} else {
			state = PlacementState.Placing;
		}
	}

	protected void UpdatePlaceableColor() {
		if (state == PlacementState.Placed) {
			return;
		}

		mat.color = state == PlacementState.Collision ? invalidColor : matColor;
	}

	protected void FollowMarker() {
		if (this.marker == null || this.state == PlacementState.Placed) {
			return;
		}
		transform.position = marker.position;
	}

	public void SetMarker(Transform m) {
		this.marker = m;
	}

	protected virtual void OnTriggerEnter(Collider c) {
		if (state == PlacementState.Placed) {
			return;
		}

		if (collisions == null) {
			collisions = new List<Collider> ();
		}

		collisions.Add (c);
	}

	protected virtual void OnTriggerExit(Collider c) {
		if (state == PlacementState.Placed) {
			return;
		}

		if (collisions == null) {
			collisions = new List<Collider> ();
		}

		collisions.Remove (c);
	}


	protected void SetObjectCollision(bool v) {
		coll.isTrigger = !v;
		obs.enabled = v;
	}
}
