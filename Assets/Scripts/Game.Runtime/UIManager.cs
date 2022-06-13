using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject  victoryUI;
    [SerializeField] private GameObject  defeatedUI;
    [SerializeField] private GameManager GM;
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
            victoryUI.SetActive(GM.isVictory);
            defeatedUI.SetActive(GM.isDefeated);
        
    }
    public void Replay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
