using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PausePannel;
    public GameObject GameOverPannel;
    public GameObject WinPannel;
    public bool isPaused;

    public AudioSource GameMusicSrc;
    public AudioClip GameMusicClip;

    public AudioSource EndMusicSrc;
    public AudioClip EndMusicClip;


    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        PausePannel.SetActive(false);

        GameOverPannel.SetActive(false);
        WinPannel.SetActive(false);

        Time.timeScale = 1;

        GameMusicSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PausePressed()
    {
        // Pause

        if (!isPaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            isPaused = true;
            Time.timeScale = 0;
            PausePannel.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            isPaused = false;
            Time.timeScale = 1;
            PausePannel.SetActive(false);
        }
    }

    public void PlayPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenuPressed()
    {
        isPaused = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    void EndGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        isPaused = true;

        Time.timeScale = 0;
        GameMusicSrc.Stop();
        EndMusicSrc.Play();
    }

    public void WinGame()
    {
        EndGame();
        WinPannel.SetActive(true);
    }

    public void GameOver()
    {
        EndGame();
        GameOverPannel.SetActive(true);
    }
}
