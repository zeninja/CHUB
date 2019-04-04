using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;


public class Raylabyrinth : MonoBehaviour
{
    public RaymarchObject[] halls;
    public RaymarchObject[] corners;

    public float hallWidth = 5, hallLength = 20;

    void Update()
    {
        SetHallCutoutSize();
        UpdateLabyrinthWidthAndHeight();
		SetHallPositions();
        SetCornerPositions();
    }

    void SetHallCutoutSize()
    {
        foreach (RaymarchObject hallwayCutout in halls)
        {
            hallwayCutout.GetObjectInput("x").SetFloat(hallLength);
            hallwayCutout.GetObjectInput("z").SetFloat(hallWidth);
        }
    }


	public float labWidth = 30, labHeight = 30;

    void UpdateLabyrinthWidthAndHeight() {
        labHeight = hallLength * 2;
        labWidth  = hallLength * 2;
    }

	void SetHallPositions() {

		float w = labWidth / 2;
		float h = labHeight / 2;

		halls[0].transform.localPosition = new Vector3(  0, 0,  w);
		halls[1].transform.localPosition = new Vector3(  h, 0,  0);
		halls[2].transform.localPosition = new Vector3(  0, 0, -w);
		halls[3].transform.localPosition = new Vector3( -h, 0,  0);

	}

    void SetCornerPositions() {
        float w = labWidth / 2;
		float h = labHeight / 2;

		corners[0].transform.localPosition = new Vector3(  w, 0,  h);
		corners[1].transform.localPosition = new Vector3(  w, 0, -h);
		corners[2].transform.localPosition = new Vector3( -w, 0, -h);
		corners[3].transform.localPosition = new Vector3( -w, 0,  h);

    }

    // void SetLabPositions() {
    //     		cutouts[0].transform.localPosition = new Vector3( 0, 0,  w);
	// 	cutouts[1].transform.localPosition = new Vector3( h, 0,  0);
	// 	cutouts[2].transform.localPosition = new Vector3( 0, 0, -w);
	// 	cutouts[3].transform.localPosition = new Vector3(-h, 0,  0);
    // }
}
