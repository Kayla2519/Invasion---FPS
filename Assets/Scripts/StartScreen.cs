using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public Button startButton;
    public Button closeGameButton;
    public Button howToPlayButton;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartButtonClicked);
        }

        if (closeGameButton != null)
        {
            closeGameButton.onClick.AddListener(CloseGameButtonClicked);
        }
    }

    public void StartButtonClicked()
    {
        SceneManager.LoadScene("IntroScript");
    }

    public void HowToPlayButtonClicked()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void CloseGameButtonClicked()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }
}
