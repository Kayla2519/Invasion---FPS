using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Button playAgainButton;
    public Button quitButton;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(PlayAgainButtonClicked);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitButtonClicked);
        }
    }

    public void PlayAgainButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitButtonClicked()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
