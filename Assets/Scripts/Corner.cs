using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour {

	public GameObject leftWall, rightWall, floor;

	public float wallWidth;
	public float wallHeight;
	public float hallLength;
	public float crnrWidth;

	void Start()
    {
        SpawnCorner();
    }

    void SpawnCorner()
    {

		leftWall.transform.parent  = transform;
		rightWall.transform.parent = transform;
		floor.transform.parent     = transform;

        leftWall .transform.localScale = new Vector3(wallWidth, wallHeight, crnrWidth);
        rightWall.transform.localScale = new Vector3(wallWidth, wallHeight, crnrWidth);
        floor	 .transform.localScale = new Vector3(crnrWidth, wallWidth,  crnrWidth);

        leftWall .transform.localPosition = new Vector3(-crnrWidth / 2, wallHeight / 2, 0);
        rightWall.transform.localPosition = new Vector3(		 	 0, wallHeight / 2, crnrWidth / 2);
        floor	 .transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update() {
        GetSettings();
        UpdateCorner();
    }

    void GetSettings() {

    }

    void UpdateCorner() {
        leftWall .transform.localScale = new Vector3(wallWidth, wallHeight, crnrWidth);
        rightWall.transform.localScale = new Vector3(wallWidth, wallHeight, crnrWidth);
        floor	 .transform.localScale = new Vector3(crnrWidth, wallWidth,  crnrWidth);

        leftWall .transform.localPosition = new Vector3(-crnrWidth / 2, wallHeight / 2, 0);
        rightWall.transform.localPosition = new Vector3(		 	 0, wallHeight / 2, crnrWidth / 2);
        floor	 .transform.localPosition = new Vector3(0, 0, 0);
    }

}
