using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAbility : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    [FormerlySerializedAs("damage")] public int attackPower;
    public float moveSpeed = 2f;
    public float chaseSpeed = 2f;
    public bool mustPatrol;
    private bool attacking;
    private Rigidbody2D _enemyRB;
    private int currentPoint = 0;
    private int nextPoint = 1;
    private int temp;
    private bool _isChase;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private HpBarManager characterHpBar;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private bool flipped;
    [SerializeField] private float visibleRange;
    [SerializeField] private CharacterAbility player;
    [SerializeField] private float attackRange;
    [FormerlySerializedAs("AttackPoint")] [SerializeField] private Transform attackPoint;
    public List<Vector2> waypoints = new List<Vector2>();
    private static readonly int s_hurt = Animator.StringToHash("Hurt");
    private static readonly int s_walk = Animator.StringToHash("Walk");
    private static readonly int s_death = Animator.StringToHash("Death");
    
    private void Start()
    {
        currentHealth = this.maxHealth;
        this.characterHpBar.SetMaxHealth(this.maxHealth);
        _enemyRB = GetComponent<Rigidbody2D>();
        currentHealth = this.maxHealth;
        this.flipped = true;
        this.attacking = false;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        this.enemyAnimator.SetTrigger(s_hurt);
      
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        this._enemyRB.simulated = false;
        enemyAnimator.SetTrigger(s_death);
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    private void Update()
    {
        
            float distanceToPlayer = Vector2.Distance(transform.position, this.player.gameObject.transform.position);
        
            if (distanceToPlayer < this.visibleRange&&!this.attacking)
            {
                this._isChase = true;
                ChasePlayer();
            
            }
            else
            {
                StopChasePlayer();
            }

            if (distanceToPlayer < 2)
            {
                AttackPlayer();
            }
        
       
            this.characterHpBar.SetHealth(this.currentHealth);
            this.enemyAnimator.SetFloat(s_walk,Mathf.Abs(this._enemyRB.velocity.x));
            if (this.mustPatrol)
            {
                MoveToTarget(this.waypoints[currentPoint],this.moveSpeed);
                float distance = Vector2.Distance(transform.position, this.waypoints[currentPoint]);
                if (Mathf.Abs(distance) <= 0.5f)
                {
                    Debug.Log("Change " + distance);
                    temp = currentPoint;
                    currentPoint = nextPoint;
                    nextPoint = temp;
                }
            }
    }

   void  AttackPlayer()
   {
       if (this.player.playerDead)
       {
           return;
       }
       this.mustPatrol = false;
       this.attacking = true;
       MoveToTarget(this.player.gameObject.transform.position,0);
       this.enemyAnimator.SetTrigger("Attack");
   }

  public void DealingDameToPlayer()
   {
       Collider2D[] PlayerbeAttacked =
           Physics2D.OverlapCircleAll(this.attackPoint.transform.position, attackRange, this.playerMask);
       foreach (var player in PlayerbeAttacked)
       {
           player.GetComponent<CharacterAbility>().GetDamage(this.attackPower);
       }
   }

   private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.09f, 0.11f, 1f);
        foreach (var t in this.waypoints)
        {
            Gizmos.DrawWireSphere(t, .2f);
        }
    }

    private void MoveToTarget(Vector2 targetPosition, float speed)
    {
        float distanceToTarget = Vector2.Distance(transform.position,targetPosition);
        if ((MathF.Abs(distanceToTarget) > 0.5f))
        {
            if (this.transform.position.x < targetPosition.x)
            {
                if (this.flipped)
                {
                    LookAtTarget();
                }

                this._enemyRB.velocity = Vector2.right * (speed * Time.fixedDeltaTime);
            }
            else if (this.transform.position.x > targetPosition.x)
            {
                if (!this.flipped)
                {
                    LookAtTarget();
                }
                this._enemyRB.velocity = -Vector2.right * (speed * Time.fixedDeltaTime);
            }
        }
    }


    private void LookAtTarget()
    {
        this.transform.Rotate(Vector3.up, 180);
        this.flipped = !this.flipped;
    }

    private void ChasePlayer()
    {
        if (this._isChase)
        {
            mustPatrol = false;
            MoveToTarget(this.player.gameObject.transform.position,this.chaseSpeed);
            
        }
    }
    private void StopChasePlayer()
    {
        this.mustPatrol = true;
        this.attacking = false;
    }
}