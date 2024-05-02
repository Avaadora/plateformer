using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct_Plateform : Plateform
{
    [SerializeField] float FallingDelay = 3f;
    [SerializeField] float ResetDelay = 3f;
    private Rigidbody2D Rb;
    private bool IsWaitinfForFall;
    private Vector2 OriginalPostion;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Rb = GetComponent<Rigidbody2D>();

        OriginalPostion = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsWaitinfForFall)
        {
            IsWaitinfForFall = true;
            Invoke("Fall", FallingDelay); //OPTI : Coroutine
        }
    }
    private void Fall()
    {
        collider2D.enabled = false;
        Rb.bodyType = RigidbodyType2D.Dynamic;

        Invoke("Respawn", ResetDelay);
    }
    private void Respawn()
    {
        Rb.velocity = Vector2.zero;
        Rb.angularVelocity = 0;
        Rb.bodyType = RigidbodyType2D.Static;
        transform.position = OriginalPostion;
        collider2D.enabled = true;       
    }
}
