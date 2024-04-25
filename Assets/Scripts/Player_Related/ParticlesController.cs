using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] ParticleSystem ParticleMovment;

    [Range(0,10)]
    [SerializeField] int SpeedForDust;

    [Range(0,0.2f)]
    [SerializeField] float DustFormationPeriod;

    [SerializeField] Rigidbody2D PlayerRB;

    float count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;

        if (GameManager.Instance.getIsGrounded() && Mathf.Abs(PlayerRB.velocity.x) > SpeedForDust)
        {
            if (count > DustFormationPeriod)
            {
                ParticleMovment.Play();
                count = 0;
            }
            
        }
        
    }
}
