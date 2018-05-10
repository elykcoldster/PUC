using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : PersistentSingleton<GameUI> {

	[SerializeField]
	protected Canvas statsUI;
	[SerializeField]
	protected GameObject placementUI;
	[SerializeField]
	protected GameObject menuUI;
	[SerializeField]
	protected GameObject popupUI;
	[SerializeField]
	protected Button play;
	[SerializeField]
	protected Button pause;
	[SerializeField]
	protected Button reset;
	[SerializeField]
	protected Text levelText;
	[SerializeField]
	protected Text moneyText;
	[SerializeField]
	protected Text popupText;
	[SerializeField]
	protected GameObject contextMenu;

	protected Animator popupAnimator;

	protected override void Awake ()
	{
		base.Awake ();

		play.onClick.AddListener (Play);
		pause.onClick.AddListener (Pause);
		reset.onClick.AddListener (Stop);

		InitLevelText ();
		InitMoneyText ();
		InitPopupText ();
	}

	// Update is called once per frame
	void Update () {
		if (GameController.Instance != null) {
			placementUI.SetActive (GameController.Instance.gameState != GameState.Playing);
		}
	}

	protected void InitLevelText() {
		levelText.text = "Level: 1";
	}

	protected void InitMoneyText() {
		string money = string.Format ("{0:#,###0}", GameManager.STARTING_MONEY);
		moneyText.text = "$" + money;
	}

	protected void InitPopupText () {
		popupAnimator = popupText.GetComponent<Animator> ();
		popupAnimator.speed = 1.5f;

		popupText.GetComponent<RectTransform> ().localScale = Vector3.zero;
	}

	public void AddPlacementUIObject(Transform t) {
		t.SetParent (placementUI.transform);
	}

	public void Play() {

		if (GameController.Instance.gameState == GameState.Menu) {
			GameManager.Instance.SetBarnsCapacity ();
		}
		GameController.Instance.SetGameState (GameState.Playing);

		if (GridPlacement.Instance != null) {
			GridPlacement.Instance.Cancel ();
		}

		play.interactable = false;
		pause.interactable = true;
	}

	public void Stop() {
		if (Attractor.Instance != null) {
			Destroy (Attractor.Instance.gameObject);
		}

		GameManager.Instance.ClearAllChickens ();
		GameController.Instance.SetGameState (GameState.Menu);

		play.interactable = true;
		pause.interactable = true;
	}

	public void Pause() {
		if (GameController.Instance.gameState != GameState.Playing) {
			return;
		}

		GameController.Instance.SetGameState (GameState.Pause);

		play.interactable = true;
		pause.interactable = false;
	}

	public void LosePopup() {
		popupText.text = "BAAWWK!!";
		popupAnimator.SetTrigger ("play");
	}

	public void WinPopup() {
		popupText.text = "GOOD JOB!!";
		popupAnimator.SetTrigger ("play");
	}

	public void UpdateMoney() {
		string money = string.Format ("{0:#,###0}", GameManager.Instance.Points);
		moneyText.text = "$" + money;
	}

	public void UpdateLevel() {
		levelText.text = "Level: " + GameManager.Instance.Level.ToString ();
	}

	public void CreateContextMenu(Placeable p) {
		Instantiate (contextMenu, statsUI.transform);
		ContextMenu.Instance.SetContext (p.gameObject);
	}

}
