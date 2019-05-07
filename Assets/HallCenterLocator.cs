using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class HallCenterLocator : MonoBehaviour
{

        public enum HallCenter { one, two, three, four, none }
        public HallCenter targetHall = HallCenter.none;

        void Start() {
            if(targetHall != HallCenter.none) {
                GetComponent<FollowTarget>().target = HallDilator.GetInstance().transform.GetChild((int)targetHall).transform;
            }
        }
}
