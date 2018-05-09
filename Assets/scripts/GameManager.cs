using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	public const int TOTAL_HEALTH = 100;
	public const int STARTING_MONEY = 200;

	[SerializeField]
	protected GameObject chicken;
	[SerializeField]
	protected GameObject attractor;
	[SerializeField]
	protected GameObject gridPlacement;
	[SerializeField]
	protected Transform chickenTarget;

	public GameObject PlacedObjects {
		private set;
		get;
	}

	protected List<Barn> barns;

	protected int level, points, pointMultiplier;

	protected override void Awake() {
		base.Awake ();
		this.points = STARTING_MONEY;
		this.pointMultiplier = 1;
		this.level = 1;

		PlacedObjects = new GameObject ();
		PlacedObjects.name = "PlacedObjects";
	}

	public GameObject Chicken {
		get{ return this.chicken; }
	}

	public GameObject Attractor {
		get{ return this.attractor; }
	}

	public Transform ChickenTarget {
		get{ return this.chickenTarget; }
	}

	public GameObject GridPlacementPrefab {
		get{ return this.gridPlacement; }
	}

	public int Level {
		get { return this.level; }
	}

	public int Points {
		get { return this.points; }
	}

	public void ClearAllChickens() {
		GameObject[] chickens = GameObject.FindGameObjectsWithTag ("Chicken");

		foreach (GameObject chicken in chickens) {
			Destroy (chicken);
		}
	}

	public void IncreaseLevel() {
		this.level++;
		GameUI.Instance.UpdateLevel ();
	}

	public int GetLevelCapacity() {
		return 5 * (level + 1);
	}

	public void LevelUp () {
		GameUI.Instance.Stop ();
		GameUI.Instance.WinPopup ();
		this.IncreaseLevel ();
	}

	public void AddBarn(Barn b) {
		if (this.barns == null) {
			this.barns = new List<Barn> ();
		}

		this.barns.Add (b);
	}

	public void SetBarnsCapacity() {
		foreach (Barn b in barns) {
			b.SetCapacity (GetLevelCapacity ());
		}
	}

	public bool BarnsEmpty() {
		foreach (Barn b in barns) {
			if (b.Capacity > 0) {
				return false;
			}
		}

		return true;
	}

	public void GainPoints() {
		this.points += (15 + Random.Range (-3, 4)) * pointMultiplier;
		GameUI.Instance.UpdateMoney ();
	}

	public void GainPoints(int n) {
		this.points += n;
		GameUI.Instance.UpdateMoney ();
	}

	public bool SpendPoints(int n) {
		if (n > this.points) {
			return false;
		}

		this.points -= n;
		GameUI.Instance.UpdateMoney ();

		return true;
	}

	public void IncreaseMultiplier() {
		this.pointMultiplier++;
	}

}
