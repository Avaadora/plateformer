using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float FallGravityScaleMultiplier = 1f;
    private float WallCheckHeight = 0.1f;
    private float WallCheckWidth = 1f;
    private float GroundCheckWidth = 0.5f;
    private float GroundCheckHeight = 0.1f;
    private float HorizontalInput, GravityScale;


    [SerializeField] private bool isTouchingWallLeft;
    [SerializeField] private bool isTouchingWallRight;
    private bool isFacingRight;

    [SerializeField] private LayerMask JumpLayerMask;
    [SerializeField] private LayerMask WallLayerMask;

    private Vector2 GroundCheckPosition;
    private Vector2 WallCheckPositionLeft;
    private Vector2 WallCheckPositionRight;
    private Vector2 zeroVelocity = Vector2.zero;


    private Rigidbody2D RbPlayer;

    private InputController MyInputActions;

    void Awake()
    {
        RbPlayer = GetComponent<Rigidbody2D>();
        GravityScale = RbPlayer.gravityScale;

        MyInputActions = new InputController();
        MyInputActions.Player.Enable();

        MyInputActions.Player.Jump.started += ctx => StartJump();
        MyInputActions.Player.Jump.canceled += ctx => StopJump();

        MyInputActions.Player.Glide.started += ctx => StartGlide();
        MyInputActions.Player.Glide.canceled += ctx => StopGlide();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGroundCheckOffset();
        UpdateCheckPosition();

        HorizontalInput = MyInputActions.Player.HorizontalMove.ReadValue<float>();

        animator.SetFloat("Speed", Mathf.Abs(HorizontalInput));

        Collider2D col = Physics2D.OverlapBox(GroundCheckPosition, new Vector2(GroundCheckWidth, GroundCheckHeight), 0, JumpLayerMask); //Overlap -> boîte fictive qui permets de checks si le player est en collision avec le sol ou pas pour le faire sauter
        Collider2D colLeft = Physics2D.OverlapBox(WallCheckPositionLeft, new Vector2(WallCheckWidth, WallCheckHeight), 0, WallLayerMask);
        isTouchingWallLeft = (colLeft != null);
        Collider2D colRight = Physics2D.OverlapBox(WallCheckPositionRight, new Vector2(WallCheckWidth, WallCheckHeight), 0, WallLayerMask);
        isTouchingWallRight = (colRight != null);

        // isGrounded = (col != null); // If statement plus court
        if (col != null)
        {
            // Sol sous les pieds
            GameManager.Instance.setIsGrounded(true);
        }
        else if (GameManager.Instance.getIsGrounded())
        {
            // En l'air mais a sauté donc sol sous les pieds
            StartCoroutine(UpdateisGroundedState(false)); //Assynchrone
        }

    }

    private void FixedUpdate()
    {
        // DÉPLACEMENT
        if (!(HorizontalInput > 0 && isTouchingWallRight) || (HorizontalInput < 0 && isTouchingWallLeft))
        {
            Flip();
            Move();
        }

        // SAUTER
        if (MyInputActions.Player.Jump.IsPressed() && GameManager.Instance.getIsGrounded())
        {
            GameManager.Instance.setIsJumping(true);
        }
        else
        {
            GameManager.Instance.setIsJumping(false);
        }

        // PLANER
        if (MyInputActions.Player.Glide.ReadValue<float>() > 0 && !GameManager.Instance.getIsGrounded() && RecipeManager.Instance.getCanGlide())
        {
            RecipeManager.Instance.setIsGliding(true);
            StartCoroutine(UpdateisJumpState(false));
        }
        else
        {
            RecipeManager.Instance.setIsGliding(false);
        }

        // WALL JUMP
        if (isTouchingWallLeft && MyInputActions.Player.Jump.IsPressed())
        {
            WallJump(Vector2.right);
        }

        if (isTouchingWallRight && MyInputActions.Player.Jump.IsPressed())
        {
            WallJump(Vector2.left);
        }


        if (GameManager.Instance.getIsJumping())
        {
            Jump();
        }

        if (RbPlayer.velocity.y < 0)
        {
            // En train de descendre (rechute) -> pour redescendre plus vite que le saut
            RbPlayer.gravityScale = GravityScale * FallGravityScaleMultiplier;

            // Fix de la vélocité en y pour ne pas passer à travers les plateformes
            // RbPlayer.velocity = new Vector2(0f, RbPlayer.velocity.y);

            if (RecipeManager.Instance.getIsGliding())
            {
                RbPlayer.gravityScale = 0;
                Glide();
            }
        }
        else
        {
            // En saut ou au sol (montée)
            RbPlayer.gravityScale = GravityScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tuto"))
        {
            GameManager.Instance.DialogueScene();
        }

        if (other.CompareTag("Dig"))
        {
            GameManager.Instance.DialogueScene();
        }
    }

    // Permet de temporiser un certain temps (CoyoteTime), sur le saut hors d'une plate-forme 
    private IEnumerator UpdateisGroundedState(bool isGroundedState)
    {
        yield return new WaitForSeconds(GameManager.Instance.getCoyoteTime());
        GameManager.Instance.setIsGrounded(isGroundedState);
    }

    private IEnumerator UpdateisJumpState(bool isJumpState)
    {
        yield return new WaitForSeconds(GameManager.Instance.getCoyoteTime());
        GameManager.Instance.setIsJumping(isJumpState);
    }

    // Déplacement du joueur sur l'axe horizontal
    // OPTI : Tout mettre dans le FixedUpdate()
    private void Move()
    {
        Vector2 targetVelocity = new Vector2(HorizontalInput * GameManager.Instance.getSpeed(), RbPlayer.velocity.y);
        RbPlayer.velocity = Vector2.SmoothDamp(RbPlayer.velocity, targetVelocity, ref zeroVelocity, GameManager.Instance.getSmoothing());
    }

    private void Jump()
    {
        //Vector2 horizontalVelocity = new Vector2(RbPlayer.velocity.x, 0f);
        //RbPlayer.velocity = horizontalVelocity.normalized * Mathf.Abs(GameManager.Instance.getSmoothing()) + Vector2.up * GameManager.Instance.getJumpForce();

        RbPlayer.velocity = new Vector2(RbPlayer.velocity.x, GameManager.Instance.getJumpForce());
    }

    private void Glide()
    {
        RbPlayer.velocity = new Vector2(RbPlayer.velocity.x, -GameManager.Instance.getGlideSpeed());
    }

    private void WallJump(Vector2 wallDirection)
    {
        Vector2 jumpDirection = Vector2.up + wallDirection;
        // jumpDirection.Normalize();

        RbPlayer.velocity = jumpDirection * GameManager.Instance.getJumpForce();
    }

    private void UpdateGroundCheckOffset()
    {
        // SpriteRenderer spritePlayer = GetComponent<SpriteRenderer>(); // Récupération du sprite du joueur
        float height = RbPlayer.transform.localScale.y; // Récupération de la hauteur du sprite du joueur
        GroundCheckPosition = new Vector2(transform.position.x, transform.position.y - height / 3f);
    }

    private void UpdateCheckPosition()
    {
        // SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float largeur = RbPlayer.transform.localScale.x;
        float hauteur = RbPlayer.transform.localScale.y;

        GroundCheckPosition = new Vector2(transform.position.x, transform.position.y - hauteur / 2f);
        WallCheckPositionLeft = new Vector2(transform.position.x - largeur / 2f, transform.position.y);
        WallCheckPositionRight = new Vector2(transform.position.x + largeur / 2f, transform.position.y);
    }

    void Flip()
    {
        if (isFacingRight && HorizontalInput < 0f || !isFacingRight && HorizontalInput > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void StartJump()
    {
        GameManager.Instance.setIsJumping(true);
    }

    private void StopJump()
    {
        GameManager.Instance.setIsJumping(false);
    }

    private void StartGlide()
    {
        RecipeManager.Instance.setIsGliding(true);
    }

    private void StopGlide()
    {
        RecipeManager.Instance.setIsGliding(false);
    }

    // Callback to draw gizmos that are pickable and always drawn.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(GroundCheckPosition, new Vector2(GroundCheckWidth, GroundCheckHeight));
        Gizmos.DrawCube(WallCheckPositionRight, new Vector2(WallCheckWidth, WallCheckHeight));
        Gizmos.DrawCube(WallCheckPositionLeft, new Vector2(WallCheckWidth, WallCheckHeight));
    }
}
