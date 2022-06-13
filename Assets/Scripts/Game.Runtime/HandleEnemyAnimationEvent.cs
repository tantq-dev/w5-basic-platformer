using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyAnimationEvent : MonoBehaviour
{
    public EnemyAbility enemy;
    public void Attack()
    {
        this.enemy.DealingDameToPlayer();
    }
}
