using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour {

	[SerializeField]
	protected GameObject placementPrefab;
	[SerializeField]
	protected Vector3 eulerRotation;
	[SerializeField]
	protected Vector3 camRotation = new Vector3(0f, 0f, 180f);

	public Vector3 EulerRotation {
		get { return this.eulerRotation; }
	}

	public Vector3 CamRotation {
		get { return this.camRotation; }
	}

	public GameObject PlacementPrefab {
		get { return this.placementPrefab; }
	}

}
