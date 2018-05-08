using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Singleton<HealthBar> {

	[SerializeField]
	protected Text healthText;
	protected Slider healthSlider;

	protected override void Awake() {
		base.Awake ();
		InitializeHealth ();
	}

	public void InitializeHealth() {
		healthSlider = GetComponent<Slider> ();
		healthSlider.maxValue = GameManager.TOTAL_HEALTH;
		healthSlider.value = GameManager.TOTAL_HEALTH;

		healthText.text = healthSlider.value.ToString ();
	}

	public void UpdateHealth() {
		// healthSlider.value = GameManager.Instance.Health;
		healthText.text = healthSlider.value.ToString ();
	}
}
