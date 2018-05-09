using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {
	Pause,
	Playing,
	Menu,
}

public enum MouseState {
	Default,
	UI
}

public class GameController : Singleton<GameController> {

	protected static float GUIObjectSpacing = 10f;
	protected static Vector3 GUIObjectPosition = new Vector3 (500f, 500f, 0f);

	[SerializeField]
	protected GameObject GUICameraObject;
	[SerializeField]
	protected GameObject GUIButtonBackground;
	[SerializeField]
	protected GameObject[] GUIObjects;

	public GameState gameState;
	public MouseState mouseState;

	protected override void Awake() {
		base.Awake ();

		this.gameState = GameState.Menu;

		InitGUI ();
	}

	protected void Update() {
		CheckChickensRemain ();
	}

	protected void InitGUI() {
		for (int i = 0; i < GUIObjects.Length; i++) {
			GameObject obj = Instantiate<GameObject> (GUIObjects[i],
				GUIObjectPosition - Vector3.down * GUIObjectSpacing * i,
				GUIObjects[i].transform.rotation
			);

			GameUI.Instance.AddPlacementUIObject (obj.transform);
			CreateGUICamera (obj, i);
		}
	}

	protected void CreateGUICamera(GameObject obj, int index) {

		// Get object prefab
		GameObject prefab = obj.GetComponent<Placement>().PlacementPrefab;

		// Create new camera and set position to be centered on the GUI object
		Camera cam = (Instantiate (GUICameraObject,
			GUIObjectPosition - Vector3.forward + Vector3.up * obj.GetComponent<MeshRenderer> ().bounds.size.y / 2f,
			Quaternion.Euler (new Vector3 (0f, 0f, -45f))
		)).GetComponent<Camera> ();

		// Adjust camera position on the UI
		float aspect = (float)Screen.height / (float)Screen.width;
		cam.rect = new Rect (1- aspect * 0.1f, 0.9f - 0.1f * index, 0.1f * aspect, 0.1f);

		cam.transform.SetParent (obj.transform);

		// Add canvas to camera
		Canvas canvas = (Instantiate<GameObject>(GUIButtonBackground)).GetComponent<Canvas>();
		canvas.worldCamera = cam;
		canvas.transform.SetParent (cam.transform);

		Button b = canvas.GetComponentInChildren<Button> ();
		b.onClick.AddListener (() => CreateGridPlacement(prefab));
	}

	protected void CreateGridPlacement(GameObject prefab) {
		if (GridPlacement.Instance == null) {
			GridPlacement gp = (Instantiate (GameManager.Instance.GridPlacementPrefab)).GetComponent<GridPlacement> ();
			gp.SetPlacementPrefab (prefab);
		} else {
			GridPlacement.Instance.SetPlacementPrefab (prefab);
		}
	}

	protected void CheckChickensRemain () {
		// If conditions hold, means that the player has won
		if (gameState == GameState.Playing && GameManager.Instance.BarnsEmpty()) {
			if (GameObject.FindGameObjectsWithTag ("Chicken").Length == 0) {
				GameManager.Instance.LevelUp ();
			}
		}
	}

	public void SetGameState(GameState gs) {
		this.gameState = gs;
	}
}
