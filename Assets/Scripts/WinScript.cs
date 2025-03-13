using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    [Header("Story Setup")]
    public TMP_Text storyText;
    public float typingSpeed = 0.05f;

    private string[] winStoryLines = {
        "The battlefield falls silent.\r\nThe last alien collapses, its body dissolving into a strange, glowing mist.",
        "You stand amidst the wreckage, breathing heavily, your weapon still warm from the fight.\r\nThe mothership, looming in the sky, begins to flicker.",
        "Without its forces, it cannot sustain itself.\r\nWith a final surge of energy, it vanishes into the void, leaving Earth in peace once more.",
        "You did it. Humanity survives—because of you."

    };

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(DisplayStory(winStoryLines));
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

        SceneManager.LoadScene("WinScreen");
    }
}

// Source: Story created with ChatGPT
