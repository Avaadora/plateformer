using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float GroundCheckWidth;
    [SerializeField] private float GroundCheckHeight;
    [SerializeField] private float FallGravityScaleMultiplier = 1f;

    private float HorizontalInput;
    private float GravityScale;

    [SerializeField] private LayerMask JumpLayerMask;

    private Vector2 GroundCheckPosition;
    
    private Rigidbody2D RbPlayer;

    private InputController MyInputActions;

    // Start is called before the first frame update
    void Start()
    {
        RbPlayer = GetComponent<Rigidbody2D>();
        GravityScale = RbPlayer.gravityScale;

        MyInputActions = new InputController();
        MyInputActions.Player.Enable();

        MyInputActions.Player.Jump.started += ctx => StartJump();
        MyInputActions.Player.Jump.canceled += ctx => StopJump();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGroundCheckOffset();
        HorizontalInput = MyInputActions.Player.HorizontalMove.ReadValue<float>();

        Collider2D col = Physics2D.OverlapBox(GroundCheckPosition, new Vector2(GroundCheckWidth, GroundCheckHeight), 0, JumpLayerMask); //Overlap -> boîte fictive qui permets de checks si le player est en collision avec le sol ou pas pour le faire sauter
        // isGrounded = (col != null); //If statement plus court
        if (col != null)
        {
            //Sol sous les pieds
            GameManager.Instance.setIsGrounded(true);
        }
        else if (GameManager.Instance.getIsGrounded())
        {
            //En l'air mais a sauté donc sol sous les pieds
            StartCoroutine(UpdateisGroundedState(false)); //Assynchrone
        }

        if (MyInputActions.Player.Jump.IsPressed() && GameManager.Instance.getIsGrounded())
        {
            GameManager.Instance.setIsJumping(true);
        }
        else
        {
            GameManager.Instance.setIsJumping(false);
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (GameManager.Instance.getIsJumping())
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
        yield return new WaitForSeconds(GameManager.Instance.getCoyoteTime());
        GameManager.Instance.setIsGrounded(isGroundedState);
    }

    // Déplacement du joueur sur l'axe horizontal
    //OPTI : Tout mettre dans le FixedUpdate()
    private void Move()
    {
        Vector2 targetVelocity = new Vector2(HorizontalInput * GameManager.Instance.getSpeed(), RbPlayer.velocity.y);
        RbPlayer.velocity = Vector2.SmoothDamp(RbPlayer.velocity, targetVelocity, ref targetVelocity, GameManager.Instance.getSmoothing());
    }

    private void Jump()
    {
        RbPlayer.velocity = new Vector2(RbPlayer.velocity.x, GameManager.Instance.getJumpForce());
        // GameManager.Instance.setIsGrounded(false);
    }

    private void UpdateGroundCheckOffset()
    {
        SpriteRenderer spritePlayer = GetComponent<SpriteRenderer>(); //Récupération du sprite du joueur
        float height = spritePlayer.bounds.size.y; //Récupération de la hauteur du sprite du joueur
        GroundCheckPosition = new Vector2(transform.position.x, transform.position.y - height / 2f);
    }

    private void StartJump()
    {
        GameManager.Instance.setIsJumping(true);
    }

    private void StopJump()
    {
        GameManager.Instance.setIsJumping(false);
    }

    // Callback to draw gizmos that are pickable and always drawn.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(GroundCheckPosition, new Vector2(GroundCheckWidth, GroundCheckHeight));
    }
}
