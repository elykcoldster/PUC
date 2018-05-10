using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableContextGUI : ContextGUI {

	[SerializeField]
	protected Text nameText;
	[SerializeField]
	protected Button refundButton;
	[SerializeField]
	protected Button moveButton;

	private int costToRefund;
	private Placeable placeable;

	protected override void Start () {
		base.Start ();
	}
	
	protected override void Update () {
		base.Update ();
	}

	public void Initialize(Placeable p) {
		base.Initialize ();

		nameText.text = p.Name;
		costToRefund = p.Cost;
		placeable = p;
	}

	public void Move() {
		GameManager.Instance.GainPoints (costToRefund);
		GameController.Instance.CreateGridPlacement (placeable.gameObject);
		Destroy (placeable.gameObject);
		Disappear ();
	}

	public void Refund() {
		GameManager.Instance.GainPoints (costToRefund);
		Disappear ();
		Destroy (placeable.gameObject);
	}
}
