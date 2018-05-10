using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableLaunchTrap : PlaceableTrap {

	protected override void Update () {
		base.Update ();
	}

	protected override void TrapAction (Transform t)
	{
		anim.SetTrigger ("trigger");
		t.GetComponent<ChickenMovement> ().CancelNavigation (true);
		t.GetComponent<Rigidbody> ().velocity = Crate.Instance.transform.position - t.position + 5 * Vector3.up;
	}
}
