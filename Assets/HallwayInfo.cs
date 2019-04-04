using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayInfo : MonoBehaviour
{

    public float length = 20, width = 5;
    public static float hall_length;
    public static float hall_width;

    void Awake() {
        hall_length = length;
        hall_width = width;
    }
}
