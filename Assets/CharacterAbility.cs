using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    public int maxHp;
    [SerializeField]private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    public int attackPower;
    private int _currentHp;
    public float attackSpeed;
    private float _coolDownTimer;
    [SerializeField] private HpBarManager characterHpBar;
    [SerializeField] private Animator _animator;
    private static readonly int s_attack = Animator.StringToHash("Attack");
    void Start()
    {
        this._currentHp = this.maxHp;
        this.characterHpBar.SetMaxHealth(this.maxHp);
    }

    void FixedUpdate()
    {
        this._coolDownTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            AttackWithAspd();
        }
    }


    private void AttackEnemy()
    {
        
        Collider2D[] hitEnemy  = Physics2D.OverlapCircleAll(this.attackPoint.transform.position, 1f, enemyLayer);
        foreach(Collider2D enemy in hitEnemy)
        {
            this._animator
            enemy.GetComponent<EnemyAbility>().GetDamage(this.attackPower);
        }
    }
    public void AttackWithAspd()
    {
        if (this._coolDownTimer > this.attackSpeed )
        {
            Attack();
        }
    }
    private void Attack()
    {
        this._animator.SetTrigger(s_attack);
        AttackEnemy();
        this._coolDownTimer = 0;
    }
}
