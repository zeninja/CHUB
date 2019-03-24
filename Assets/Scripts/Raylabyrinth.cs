using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaymarchingToolkit;


public class Raylabyrinth : MonoBehaviour
{
    public RaymarchObject[] cutouts;

    public float hallWidth = 5, hallLength = 20;

    void Update()
    {
        SetHallCutoutSize();
		SetHallPositions();
    }

    void SetHallCutoutSize()
    {
        foreach (RaymarchObject hallwayCutout in cutouts)
        {
            hallwayCutout.GetObjectInput("x").SetFloat(hallLength);
            hallwayCutout.GetObjectInput("z").SetFloat(hallWidth);
        }
    }


	public float labWidth = 30, labHeight = 30;

	void SetHallPositions() {
		float w = labWidth / 2;
		float h = labHeight / 2;

		cutouts[0].transform.localPosition = new Vector3( 0, 0,  w);
		cutouts[1].transform.localPosition = new Vector3( h, 0,  0);
		cutouts[2].transform.localPosition = new Vector3( 0, 0, -w);
		cutouts[3].transform.localPosition = new Vector3(-h, 0,  0);

	}
}
