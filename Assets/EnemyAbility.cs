using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbility : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public float damage;
    public float attackCoolDown;
    private float _coolDownTimer = Mathf.Infinity;
    public bool moveAround;
    public bool beAttack;
    private int _currentPoint = 0;
    public float moveSpeed = 2f;
    Vector3 currentPosition;
    public bool mustPatrol;
    private Rigidbody2D enemyRB;
    int currentPoint = 0;
    int nextPoint = 1;
    int temp;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private HpBarManager characterHpBar;
    [SerializeField] private LayerMask playerMask;
    private bool isFacingRight;
    public List<Vector2> waypoints = new List<Vector2>();
    private static readonly int s_hurt = Animator.StringToHash("Hurt");

    private void Start()
    {
        currentHealth = this.maxHealth;
        characterHpBar.SetMaxHealth(this.maxHealth);
        enemyRB = GetComponent<Rigidbody2D>();
        currentHealth = this.maxHealth;
        currentPosition = transform.position;
        moveAround = true;
        this.isFacingRight = true;
    }
    public void GetDamage(int damage)
    {   
        this.enemyAnimator.SetTrigger(s_hurt);
        currentHealth -= damage;
        beAttack = true;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        this.GetComponent<Collider2D>().enabled = true;
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
  
    private void FixedUpdate()
    {
        if (this.moveAround)
        {
            MoveToTarget(this.waypoints[currentPoint]);
            float distace = DistanceToTarget(this.waypoints[currentPoint].x);
            if (Mathf.Abs(distace) <= 0.5f)
            {
                Debug.Log("Change "+distace);
                temp = currentPoint;
                currentPoint = nextPoint;
                nextPoint = temp;
            }
        }
        
    }

    private void Update()
    {
        this.characterHpBar.SetHealth(this.currentHealth);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.09f, 0.11f, 1f);
        foreach (var t in this.waypoints)
        {
            Gizmos.DrawWireSphere(t, .2f);
        }
    }

    private void MoveToTarget(Vector2 targetPosition)
    {
        float distanceToTarget = DistanceToTarget(targetPosition.x);
        if ((MathF.Abs(distanceToTarget) > 0.5f))
        {
            if (this.transform.position.x<targetPosition.x)
            {

                this.enemyRB.velocity =Vector2.right * (this.moveSpeed * Time.fixedDeltaTime);
                
            }
            else if (this.transform.position.x > targetPosition.x)
            {
                this.enemyRB.velocity =-Vector2.right * (this.moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    float DistanceToTarget(float targetPositionX)
    {
        return this.transform.position.x - targetPositionX;
    }

    void LookAtTarget()
    {
        this.transform.Rotate(Vector3.up,180);
        this.isFacingRight = !this.isFacingRight;
    }
    
}
