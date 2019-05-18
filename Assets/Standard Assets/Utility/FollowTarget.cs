using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);

        public bool keepGrounded;
        public float groundHeight;

        private void LateUpdate()
        {
            if(target == null) { return; }
            transform.position = target.position + offset;

            if(keepGrounded) {
                Vector3 temp = transform.position; 
                temp.y = groundHeight;
                transform.position = temp;
            }
        }
    }
}
