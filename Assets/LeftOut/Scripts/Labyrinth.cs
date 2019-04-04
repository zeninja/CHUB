using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Labyrinth : MonoBehaviour {

	public float length;
	public float width;
	public float hallwayWidth = 5;
	public HallwayController top, bot, left, right;

	// Update is called once per frame
	void Update () {
		UpdateHalls();
	}

	void UpdateHalls() {
		left .transform.position = new Vector3(-width / 2 - hallwayWidth / 2, 0, 0);
		right.transform.position = new Vector3( width / 2 + hallwayWidth / 2, 0, 0);

		top  .transform.position = new Vector3(0, 0, length / 2 + hallwayWidth / 2);
		bot  .transform.position = new Vector3(0, 0,-length / 2 - hallwayWidth / 2);

		left .hallLength = length;
		right.hallLength = length;
		top  .hallLength = width;
		bot  .hallLength = width;
		
		left .hallWidth = hallwayWidth;
		right.hallWidth = hallwayWidth;
		top  .hallWidth = hallwayWidth;
		bot  .hallWidth = hallwayWidth;
	}
}
