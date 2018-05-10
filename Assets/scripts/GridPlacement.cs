using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacement : Singleton<GridPlacement> {

	protected GameObject placementPrefab;

	protected Placeable prefabInstance;

	// Update is called once per frame
	protected void Update () {
		SnapToGrid ();
	}

	protected void InitPrefab() {
		if (this.prefabInstance != null && this.prefabInstance.State != PlacementState.Placed) {
			Destroy (prefabInstance.gameObject);
		}

		this.prefabInstance = (Instantiate<GameObject> (
			this.placementPrefab,
			this.transform.position-Vector3.up,
			Quaternion.identity
		)).GetComponent<Placeable> ();
		this.prefabInstance.SetMarker (this.transform);
	}

	protected void SnapToGrid() {
		RaycastHit hit;
		int layerMask = 1 << LayerMask.NameToLayer ("Ground");

		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, layerMask)) {
			float x = Mathf.Round (hit.point.x / 0.5f) * 0.5f,
			y = hit.point.y,
			z = Mathf.Round (hit.point.z / 0.5f) * 0.5f;

			transform.position = new Vector3 (x, y, z);
		}
	}

	public void SetPlacementPrefab(GameObject prefab) {
		this.placementPrefab = prefab;
		InitPrefab ();
	}

	public void PlacementComplete() {
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.LeftShift)) {
			InitPrefab ();
		} else {
			Destroy (gameObject);
		}
	}

	public void Cancel() {
		if (prefabInstance.State != PlacementState.Placed) {
			Destroy (prefabInstance.gameObject);
		}

		Destroy (gameObject);
	}
}
