using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HandleEnemyAnimationEvent : MonoBehaviour
{
    public EnemyAbility enemy;
    private Animator _animator;

    private void Start()
    {

        this._animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        this.enemy.DealingDameToPlayer();
    }
}
