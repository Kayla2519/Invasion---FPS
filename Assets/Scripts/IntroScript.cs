using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [Header("Story Setup")]
    public TMP_Text storyText;
    public float typingSpeed = 0.05f;

    private string[] introStoryLines = {
        "In the year 2150, Earth’s defenses were unprepared for an invasion from beyond the stars.",
        "An alien force has landed. Their ship looms in the distance, casting a shadow over the land.",
        "You are humanity’s last hope—armed only with a basic firearm, it’s up to you to take down the invaders.",
        "Survive the onslaught, collect supplies, and score points by eliminating as many of the alien forces as you can.",
        "Reach 2000 points... or face certain death."
    };

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(DisplayStory(introStoryLines));
    }

    private IEnumerator DisplayStory(string[] storyLines)
    {
        foreach (string line in storyLines)
        {
            storyText.text = "";
            foreach (char letter in line.ToCharArray())
            {
                storyText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitUntil(() => Input.anyKeyDown);
        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Game");
    }
}

// Source: Story created with ChatGPT
