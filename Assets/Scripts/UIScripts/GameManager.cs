using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PausePannel;
    public bool isPaused;

    private void Awake()
    {
        isPaused = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        PausePannel.SetActive(false);
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

    public void MainMenuPressed()
    {
        isPaused = false;
        SceneManager.LoadScene("MainMenuScene");
    }
}
