using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndgameUI : MonoBehaviour
{
    [SerializeField] private GameObject  victoryUI;
    [SerializeField] private GameObject  defeatedUI;
    [FormerlySerializedAs("GM")] [SerializeField] private GameManager gm;
    
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
            victoryUI.SetActive(this.gm.isVictory);
            defeatedUI.SetActive(this.gm.isDefeated);
    }
    public void Replay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
