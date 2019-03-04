using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HallwayController : MonoBehaviour
{

    public float hallLength;
    public float hallWidth;
    public float wallHeight;
    public float wallWidth;
    public Corner corner;


    void Start()
    {
        SpawnHallway();
        // corner.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
    }

    void SpawnHallway()
    {
        // leftWall  = Instantiate(prefab);
        // rightWall = Instantiate(prefab);
        // floor 	  = Instantiate(prefab);

		leftWall.transform.parent = transform;
		rightWall.transform.parent = transform;
		floor.transform.parent = transform;


        leftWall .transform.localScale = new Vector3(wallWidth, wallHeight, hallLength);
        rightWall.transform.localScale = new Vector3(wallWidth, wallHeight, hallLength);
        floor	 .transform.localScale = new Vector3(hallWidth, wallWidth, hallLength);

        leftWall .transform.localPosition = new Vector3(-hallWidth / 2, wallHeight / 2, 0);
        rightWall.transform.localPosition = new Vector3( hallWidth / 2, wallHeight / 2, 0);
        floor	 .transform.localPosition = new Vector3(0, 0, 0);
        
    }

    public GameObject leftWall, rightWall, floor;

    void Update()
    {
        leftWall .transform.localScale = new Vector3(wallWidth, wallHeight, hallLength);
        rightWall.transform.localScale = new Vector3(wallWidth, wallHeight, hallLength);
        floor	 .transform.localScale = new Vector3(hallWidth, wallWidth, hallLength);

		leftWall .transform.localPosition = new Vector3(-hallWidth / 2, wallHeight / 2, 0);
        rightWall.transform.localPosition = new Vector3( hallWidth / 2, wallHeight / 2, 0);
        floor	 .transform.localPosition = new Vector3(0, 0, 0);

        corner.transform.localPosition = new Vector3(0, 0, hallLength / 2 + hallWidth / 2);
    }
}
