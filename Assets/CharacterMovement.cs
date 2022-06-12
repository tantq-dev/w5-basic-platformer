using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    public float speed;
    private float _horizontalDir;
    private bool _isFacingRight;
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
    }

    private void FixedUpdate()
    {
        Move(this._horizontalDir);
        FlipPlayer();
        
    }

    void Move(float moveDir)
    {
        this._playerRb.velocity = new Vector2(2, 0) * (moveDir * this.speed * Time.fixedDeltaTime);
        
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
