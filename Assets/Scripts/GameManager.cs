using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   
    public GameObject CompleteLevelUI;
    public GameObject GameOverUI;
    bool gameHasEnded = false;
    public float RestartDelay = 1f;
    public int currentLevel;
    public GameObject NextLevelBtn;
    public Animation anim;
    public AnimationClip[] clips;
    public GameObject confetti;

    public void CompleteLevel()
    {
        CompleteLevelUI.SetActive(true);
        Time.timeScale = 0;
        PlayerPrefs.SetInt("Level", currentLevel);
        confetti.SetActive(true);
    }
    private void Start()
    {
        Time.timeScale = 1;
        currentLevel = PlayerPrefs.GetInt("Level");
        
        Instantiate(Resources.Load("Level" + currentLevel), new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void GameOver()
    {
        
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", RestartDelay);
        }
    }

    public void seviyeAtlama()
    {
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
