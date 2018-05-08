using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : MonoBehaviour {

	[SerializeField]
	protected Transform spawnPoint;
	[SerializeField]
	protected float spawnRate = 1f;

	private int capacity;
	private float spawnTimer = 0f;

	public int Capacity {
		get { return this.capacity; }
	}

	public Transform SpawnPoint {
		get {
			return spawnPoint;
		}
	}

	protected void Start() {
		GameManager.Instance.AddBarn (this);
	}
	
	protected void Update () {
		UpdateTimers ();
	}

	private void UpdateTimers() {

		if (GameController.Instance.gameState != GameState.Playing) {
			return;
		}

		this.spawnTimer += Time.deltaTime;

		if (spawnTimer >= 1 / spawnRate) {
			SpawnChicken ();
			spawnTimer = 0f;
		}
	}

	private void SpawnChicken() {
		if (capacity <= 0) {
			return;
		}

		Instantiate<GameObject> (GameManager.Instance.Chicken, spawnPoint.position, Quaternion.identity);
		capacity--;
	}

	public void SetCapacity(int n) {
		this.capacity = n;
	}
}
