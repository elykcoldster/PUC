using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : Singleton<ContextMenu> {

	[SerializeField]
	protected GameObject placeableContextMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetContext(GameObject o) {
		if (o.GetComponent<Placeable> () != null) {
			SetPlaceableContext (o.GetComponent<Placeable> ());
		}
	}

	private void SetPlaceableContext(Placeable p) {
		if (ContextGUI.Instance == null) {
			Instantiate<GameObject> (placeableContextMenu, transform);
		}

		if (ContextGUI.Instance.GUIState == ContextGUIState.Visible) {
			return;
		}

		((PlaceableContextGUI)(ContextGUI.Instance)).Initialize (p);
		ContextGUI.Instance.SetPosition (p.transform.position + Vector3.up * p.Size.y);
	}
}
