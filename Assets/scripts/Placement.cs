using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour {

	[SerializeField]
	protected GameObject placementPrefab;

	public GameObject PlacementPrefab {
		get { return this.placementPrefab; }
	}

}
