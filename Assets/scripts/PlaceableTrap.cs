using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlaceableTrap : Placeable {

	[SerializeField]
	protected float cooldown = 2f;

	protected Animator anim;

	private float trapTimer = 0f;

	protected override void Awake() {
		base.Awake ();
		anim = GetComponent<Animator> ();
	}

	protected override void Update () {
		base.Update ();

		UpdateTimer ();
	}

	protected void UpdateTimer() {

		if (GameController.Instance.gameState != GameState.Playing) {
			return;
		}

		trapTimer += Time.deltaTime;

		if (trapTimer >= cooldown) {
			trapTimer = cooldown;
		}
	}

	protected virtual void OnCollisionEnter(Collision c) {
		if (GameController.Instance.gameState != GameState.Playing) {
			return;
		}

		if (c.gameObject.layer == LayerMask.NameToLayer ("Chicken") && trapTimer >= cooldown) {
			if (c.gameObject.GetComponent<ChickenMovement> ().State != ChickenState.Default) {
				return;
			}

			trapTimer = 0f;
			TrapAction(c.transform);
		}
	}

	// absolutely should override!
	protected virtual void TrapAction(Transform t) {
		GameManager.Instance.GainPoints ();
		Destroy (t.gameObject);
	}
}
