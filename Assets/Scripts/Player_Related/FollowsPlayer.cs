using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowsPlayer : MonoBehaviour
{
    [SerializeField] Transform followTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        transform.position = new Vector3(followTransform.position.x, followTransform.position.y, transform.position.z);
    }
}
