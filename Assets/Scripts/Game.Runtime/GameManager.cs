using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public CharacterAbility characterAbility;
    public GameObject UI;
    [SerializeField] private GameObject enemiesCombie;
    
    public bool isVictory;
    public bool isDefeated;
    void Start()
    {
        this.isDefeated = false;
        this.isVictory = false;
    }

    
    void Update()
    {
        if (this.isDefeated || isVictory)
        {
            StartCoroutine(Wait());
        }
        this.isVictory = (this.enemiesCombie.transform.childCount==0);
        this.isDefeated = (this.characterAbility.currentHp <= 0);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
        Time.timeScale = 0;
        UI.SetActive(true);
    }
    
}
