using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class MusicController : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource shotSound;
    public AudioSource reloadingSound;

    private bool music = true;
    private bool sounds = true;

    public Button musicButton;
    public Button soundsButton;

    public Sprite offMusicIMG;
    public Sprite onMusicIMG;

    private void Start()
    {
            musicButton.image.sprite = onMusicIMG;
            soundsButton.image.sprite = onMusicIMG;
    }

    public void MusicControler()
    {
        if (sounds == true)
        {
            sounds = false;
            backgroundMusic.volume = 0;
            musicButton.image.sprite = offMusicIMG;
        }
        else
        {
            sounds = true;
            backgroundMusic.volume = 0.3f;
            musicButton.image.sprite = onMusicIMG;
        }
            
    }

    public void SoundsConroler()
    {
        if (sounds == true)
        {
            sounds = false;
            shotSound.volume = 0;
            reloadingSound.volume = 0;
            soundsButton.image.sprite = offMusicIMG;
        }
        else
        {
            sounds = true;
            shotSound.volume = 0.5f;
            reloadingSound.volume = 1f;
            soundsButton.image.sprite = onMusicIMG;
        }
    }
}