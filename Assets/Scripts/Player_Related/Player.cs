using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float JumpForce;

    [SerializeField] private float GroundCheckWidth;
    [SerializeField] private float GroundCheckHeight;

    [SerializeField] private LayerMask JumpLayerMask;

    [SerializeField] private float FallGravityScaleMultiplier = 1f;
    [SerializeField] private float CoyoteTime;

    private Rigidbody2D RbPlayer;
    private float HorizontalInput;
    private Vector2 GroundCheckPosition;
    private float GravityScale;
    
    // Start is called before the first frame update
    void Start()
    {
        RbPlayer = GetComponent<Rigidbody2D>();
        GravityScale = RbPlayer.gravityScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGroundCheckOffset();
        HorizontalInput = Input.GetAxisRaw("Horizontal");

        Collider2D col = Physics2D.OverlapBox(GroundCheckPosition, new Vector2(GroundCheckWidth, GroundCheckHeight), 0, JumpLayerMask); //Overlap -> boîte fictive qui permets de checks si le player est en collision avec le sol ou pas pour le faire sauter
        // isGrounded = (col != null); //If statement plus court
        if (col != null)
        {
            //Sol sous les pieds
            GameManager.instanceGM.setIsGrounded(true);
            // GameManager.instanceGM.setIsGrounded(true);
        }
        else if (GameManager.instanceGM.getIsGrounded())
        {
            //En l'air mais a sauté donc sol sous les pieds
            StartCoroutine(UpdateisGroundedState(false)); //Assynchrone
        }

        if (Input.GetButton("Jump") && GameManager.instanceGM.getIsGrounded())
        {
            GameManager.instanceGM.setIsJumping(true);
        }
        else
        {
            GameManager.instanceGM.setIsJumping(false);
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (GameManager.instanceGM.getIsJumping())
        {
            Jump();
        }

        if (RbPlayer.velocity.y < 0)
        {
            //En train de descendre (rechute) -> pour redescendre plus vite que le saut
            RbPlayer.gravityScale = GravityScale * FallGravityScaleMultiplier;
        }
        else
        {
            //En saut ou au sol (montée)
            RbPlayer.gravityScale = GravityScale;
        }
    }

    //Permet de temporiser un certain temps (CoyoteTime), sur le saut hors d'une plate-forme 
    private IEnumerator UpdateisGroundedState(bool isGroundedState)
    {
        yield return new WaitForSeconds(CoyoteTime);
        GameManager.instanceGM.setIsGrounded(isGroundedState);
    }

    // Déplacement du joueur sur l'axe horizontal
    //OPTI : Tout mettre dans le FixedUpdate()
    private void Move()
    {
        Vector2 targetVelocity = new Vector2(HorizontalInput * GameManager.instanceGM.getSpeed(), RbPlayer.velocity.y);
        RbPlayer.velocity = Vector2.SmoothDamp(RbPlayer.velocity, targetVelocity, ref targetVelocity, GameManager.instanceGM.getSmoothing());
    }

    private void Jump()
    {
        RbPlayer.velocity = new Vector2(RbPlayer.velocity.x, JumpForce);
    }

    private void UpdateGroundCheckOffset()
    {
        SpriteRenderer spritePlayer = GetComponent<SpriteRenderer>(); //Récupération du sprite du joueur
        float height = spritePlayer.bounds.size.y; //Récupération de la hauteur du sprite du joueur
        GroundCheckPosition = new Vector2(transform.position.x, transform.position.y - height / 2f);
    }

    // Callback to draw gizmos that are pickable and always drawn.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(GroundCheckPosition, new Vector2(GroundCheckWidth, GroundCheckHeight));
    }
}
