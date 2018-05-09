using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIButton3D : MonoBehaviour {
	public void SetMouseState(bool v) {
		GameController.Instance.mouseState = v ? MouseState.UI : MouseState.Default;
	}
}
