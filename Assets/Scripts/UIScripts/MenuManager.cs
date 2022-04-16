using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject HTPPannel;

    [SerializeField]
    GameObject CreditsPannel;

    [SerializeField]
    GameObject HTPText;

    [SerializeField]
    GameObject ControlsText;

    

    // Start is called before the first frame update
    void Start()
    {
        

        HTPPannel.SetActive(false);
        CreditsPannel.SetActive(false);

        HTPText.SetActive(true);
        ControlsText.SetActive(false);
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnHTPPressed()
    {
        HTPPannel.SetActive(true);
    }

    public void OnHTPText()
    {
        HTPText.SetActive(true);
        ControlsText.SetActive(false);
    }

    public void OnCtrlText()
    {
        HTPText.SetActive(false);
        ControlsText.SetActive(true);
    }

    public void OnCreditsPressed()
    {
        CreditsPannel.SetActive(true);
    }

    public void OnBackPressed()
    {
        HTPPannel.SetActive(false);
        CreditsPannel.SetActive(false);
    }

    public void OnExitPressed()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
