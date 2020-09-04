using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject InGamesScreen, PauseMenuUI;
    [SerializeField] private GameObject pauseMenuUI;

    private void Update()
    {


    }
    public void PauseButton()
    {
        Time.timeScale = 0;
        InGamesScreen.SetActive(false);
        PauseMenuUI.SetActive(true);

    }
    public void PlayButon()
    {

        Time.timeScale = 1;
        InGamesScreen.SetActive(true);
        pauseMenuUI.SetActive(false);

    }
    public void RestartButon()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);


    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
