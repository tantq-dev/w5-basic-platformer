using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundMask;
    private bool _grounded;
    private Rigidbody2D _playerRb;
    public float speed;
    private float _horizontalDir;
    private bool _isFacingRight;
    private  bool _jump;
    public float jumpAmount = 35;
    public float gravityScale = 10;
    public float fallingGravityScale = 40;
    
    public Animator animator;
    private static readonly int s_speed = Animator.StringToHash("Speed");
    
    void Start()
    {
        this._playerRb = GetComponent<Rigidbody2D>();
        this._isFacingRight = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        this._horizontalDir = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this._jump = true;
            
        }
        if(this._playerRb.velocity.y >= 0)
        {
            this._playerRb.gravityScale = gravityScale;
        }
        else if (this._playerRb.velocity.y < 0)
        {
            this._playerRb.gravityScale = fallingGravityScale;
        }
        
    }

    private void Grounded()
    {
        
        this._grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.groundCheck.position, this.checkRadius, this.groundMask);
        if (colliders.Length > 0)
        {
            this._grounded = true;
        }

    }
    private void FixedUpdate()
    {
        Grounded();
        this.animator.SetBool("Jump",!this._grounded);
        Debug.Log(this._grounded);
        Move(this._horizontalDir);
        FlipPlayer();
        Jump();
    }
    private void Jump()
    {
        if (this._jump&&this._grounded)    
        {
            this._playerRb.velocity = (Vector2.up*100 * jumpAmount);
            
            this._jump = false;
        }
        
    }

    void Move(float moveDir)
    {
        this._playerRb.velocity = new Vector2(2, 0) * (moveDir * this.speed );
        
        this.animator.SetFloat(s_speed,MathF.Abs(moveDir));
    }
    void FlipPlayer()
    {
        if (this._playerRb.velocity.x < 0 && this._isFacingRight)
        {
            this.transform.Rotate(Vector2.up*180);
            this._isFacingRight = !this._isFacingRight;
        }
        else if (this._playerRb.velocity.x > 0 && !this._isFacingRight)
        {
            this.transform.Rotate(Vector2.up*180);
            this._isFacingRight = !this._isFacingRight;
        }
    }
 
}

