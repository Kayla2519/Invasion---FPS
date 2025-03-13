using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoseScript : MonoBehaviour
{
    [Header("Story Setup")]
    public TMP_Text storyText;
    public float typingSpeed = 0.05f;

    private string[] loseStoryLines = {
        "Your vision blurs as you collapse to the ground.\r\nThe alien horde closes in, their eerie, inhuman voices echoing around you.",
        "The battle is lost.\r\nThe last hope of humanity fades with your final breath.",
        "The invasion continues. Earth belongs to them now."
    };

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(DisplayStory(loseStoryLines));
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

        SceneManager.LoadScene("GameOver");
    }
}

// Source: Story created with ChatGPT
