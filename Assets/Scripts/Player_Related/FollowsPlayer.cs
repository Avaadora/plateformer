using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowsPlayer : MonoBehaviour
{
    [SerializeField] Transform followTransform;
    [SerializeField] private float YAjustment;
    
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
        transform.position = new Vector3(followTransform.position.x, followTransform.position.y + YAjustment, transform.position.z);
    }
}
