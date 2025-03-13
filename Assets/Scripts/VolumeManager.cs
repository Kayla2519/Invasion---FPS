using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Image volumeOnIcon;
    public Image volumeOffIcon;

    private bool isMusicOn = true;

    void Start()
    {
        if (backgroundMusic != null && isMusicOn)
        {
            backgroundMusic.Play();
            volumeOnIcon.enabled = true;
            volumeOffIcon.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleVolume();
        }
    }

    public void ToggleVolume()
    {
        if (isMusicOn)
        {
            backgroundMusic.Pause();
            volumeOnIcon.enabled = false;
            volumeOffIcon.enabled = true;
        }
        else
        {
            backgroundMusic.Play();
            volumeOnIcon.enabled = true;
            volumeOffIcon.enabled = false;
        }

        isMusicOn = !isMusicOn;
    }
}
