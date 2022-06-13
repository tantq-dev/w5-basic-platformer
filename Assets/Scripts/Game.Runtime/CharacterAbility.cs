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
    public float attackRange;
    [SerializeField] private HpBarManager characterHpBar;
    [SerializeField] private Animator animator;
    private static readonly int s_attack = Animator.StringToHash("Attack");

    void Start()
    {
        this.currentHp = this.maxHp;
        this.characterHpBar.SetMaxHealth(this.maxHp);
        characterRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.animator.SetTrigger(s_attack);
        }
        characterHpBar.SetHealth(currentHp);
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
            Death();
        }
        this.currentHp -= damage;
        this.animator.SetTrigger("Hurt");
    }

    void Death()
    {
    
        this.animator.SetTrigger("Death");
        characterRB.simulated = false;
    }
    void GetBuff(int point, string type)
    {
        switch (type)      
        {
            case "HP":
                this.currentHp += point;
                break;
            case "AttackPower":
                this.attackPower += point;
                break;
            case "JumpForce":
                Debug.Log("Jumphigher");
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Buff"))
        {
            int point = col.GetComponent<BuffScript>().point;
            string type = col.GetComponent<BuffScript>().typeOfBuff;
            Destroy(col.gameObject);
            GetBuff(point, type);
        }
    }
}