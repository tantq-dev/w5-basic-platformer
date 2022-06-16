using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAbility : MonoBehaviour
{
    public int maxHp;
    private Rigidbody2D characterRB;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    public int attackPower;
    public int currentHp;
    public bool playerDead;
    public float attackRange;
    [SerializeField] private HpBarManager characterHpBar;
    [SerializeField] private Animator animator;
    private static readonly int s_attack = Animator.StringToHash("Attack");
    private CharacterMovement _movement;

    void Start()
    {
        this.currentHp = this.maxHp;
        this.characterHpBar.SetMaxHealth(this.maxHp);
        characterRB = GetComponent<Rigidbody2D>();
        this.playerDead = false;
        this._movement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.animator.SetTrigger(s_attack);
        }
        characterHpBar.SetHealth(currentHp);
        if (this.currentHp <= 0)
        {
            playerDead = true;
            Death();
        }
    }


    public void AttackEnemy()
    {
        Collider2D[] enemiesGetDamage =
            Physics2D.OverlapCircleAll(this.attackPoint.transform.position, attackRange, this.enemyLayer);
        foreach (var enemy in enemiesGetDamage)
        {
            enemy.GetComponent<EnemyAbility>().GetDamage(this.attackPower);
        }
    }
    public void GetDamage(int damage)
    {
        if (this.currentHp <= 0)
        {
            return;
        }

        this.currentHp -= damage;
        this.animator.SetTrigger("Hurt");

    }

    void Death()
    {

        if (this.playerDead)
        {
            this.animator.SetTrigger("Death");
            characterRB.simulated = false;
            this.gameObject.GetComponent<CharacterMovement>().speed = 0;
        }
        
        
    }
    void GetBuff(int point, string type)
    {
        switch (type)      
        {
            case "HP":
                this.currentHp += point;
                break;
            case "ATK":
                this.attackPower += point;
                break;
            case "JumpForce":
                this._movement.jumpAmount+=point;
                break;
            default:
                break;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Buff"))
        {
            int point = col.gameObject.GetComponent<BuffScript>().point;
            string type = col.gameObject.GetComponent<BuffScript>().types.ToString();
            Destroy(col.gameObject);
            GetBuff(point, type);
        }
    }
}