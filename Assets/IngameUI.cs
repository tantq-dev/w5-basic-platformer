using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    [FormerlySerializedAs("GameManager")] public GameManager gameManager;
    [SerializeField] private Text jumpText;
    [SerializeField] private Text attackText;
    [SerializeField] private GameObject player;
    private CharacterAbility _characterAbility;
    private CharacterMovement _movement;
    private float _jumpPower;
    private int _attackPower;
    
    void Start()
    {
        this.gameObject.SetActive(true);
        this._movement = this.player.GetComponent<CharacterMovement>();
        this._characterAbility = this.player.GetComponent<CharacterAbility>();
       UpdatePlayerStat();
    }
    void Update()
    {
        UpdatePlayerStat();
    }

    void UpdatePlayerStat()
    {
        this._jumpPower = this._movement.jumpAmount;
        this._attackPower = this._characterAbility.attackPower;
        this.jumpText.text = this._jumpPower.ToString();
        this.attackText.text = this._attackPower.ToString();
        if (this.gameManager.isDefeated || this.gameManager.isVictory)
        {
            this.gameObject.SetActive(false);
        }
    }
}
