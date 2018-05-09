using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ContextGUIState {
	Invisible,
	Visible,
	Transition
}

public class ContextGUI : Singleton<ContextGUI> {

	public ContextGUIState GUIState;

	protected MouseState mouseState;
	protected Animator anim;

	protected virtual void Start() {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		CheckMouseClick ();
	}

	protected void CheckMouseClick() {
		if (Input.GetMouseButtonDown (0) && GUIState == ContextGUIState.Visible && this.mouseState == MouseState.Default) {
			Disappear ();
		}
	}

	public virtual void Initialize() {
		if (GUIState == ContextGUIState.Invisible) {
			Appear ();
		}
	}

	// position is world position
	public void SetPosition(Vector3 position) {
		// print (Camera.main.WorldToViewportPoint (position));
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);

		float ratio = 1 / transform.parent.GetComponent<RectTransform> ().lossyScale.x;
		float screenHeight = Screen.height * ratio;

		if (screenPosition.y * ratio + GetComponent<RectTransform> ().rect.height > screenHeight) {
			GetComponent<RectTransform> ().pivot = new Vector2 (0.5f, 1f);
		} else {
			GetComponent<RectTransform> ().pivot = new Vector2 (0.5f, 0f);
		}

		transform.position = screenPosition;
	}

	public void SetUIState(bool v) {
		this.mouseState = v ? MouseState.UI : MouseState.Default;
	}

	public void SetContextGUIState(int state) {
		switch (state) {
		case 0:
			GUIState = ContextGUIState.Visible;
			break;
		case 1:
			GUIState = ContextGUIState.Invisible;
			break;
		case 2:
			GUIState = ContextGUIState.Transition;
			break;
		default:
			break;
		}
	}

	protected void Disappear() {
		GetComponent<Animator> ().SetTrigger ("disappear");
		GetComponent<Animator> ().ResetTrigger ("appear");
	}

	protected void Appear() {
		GetComponent<Animator> ().SetTrigger ("appear");
		GetComponent<Animator> ().ResetTrigger ("disappear");
	}
}
