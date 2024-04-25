using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowsPlayer : MonoBehaviour
{
    [SerializeField] Transform followTransform;
    
    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        transform.position = new Vector3(followTransform.position.x, followTransform.position.y, transform.position.z);
    }
}
