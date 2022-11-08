using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioClip playButton;
    public static AudioClip glass;
    public static AudioClip loseLife;
    public static AudioClip plastic;
    public static AudioClip paper;
    public static AudioClip buttonClick;
    public static AudioClip inGameMusic;
    public static AudioClip menuMusic;
    public static AudioClip regeneration;

    public Game game;

    public Slider soundSlider;
    public Slider musicSlider;

    public AudioSource soundAud;
    public AudioSource musicAud;

    public Sprite soundEnabledSprite;
    public Sprite soundDisabledSprite;
    public Sprite musicEnabledSprite;
    public Sprite musicDisabledSprite;

    public GameObject soundEnabledButton;
    public GameObject musicEnabledButton;

    public bool soundEnabled = true;
    public bool musicEnabled = true;

    private void Start()
    {
        soundSlider.value = Convert.ToSingle(game.soundsettings[0]);
        musicSlider.value = Convert.ToSingle(game.soundsettings[1]);

        soundEnabled = Convert.ToBoolean(game.soundsettings[2]);
        musicEnabled = Convert.ToBoolean(game.soundsettings[3]);

        soundAud.volume = soundSlider.value;
        musicAud.volume = musicSlider.value;

        LoadEnabledSounds();
    }

    public void Trash(string name)
    {
        switch (name)
        {
            case "Paper":
                paper = Resources.Load<AudioClip>("Paper");
                soundAud.PlayOneShot(paper);
                break;
            case "Glass":
                glass = Resources.Load<AudioClip>("Glass");
                soundAud.PlayOneShot(glass);
                break;
            case "Plastic":
                plastic = Resources.Load<AudioClip>("Plastic");
                soundAud.PlayOneShot(plastic);
                break;

        }
    }
    public void PlayButton()
    {
        playButton = Resources.Load<AudioClip>("Play Button");
        soundAud.PlayOneShot(playButton);
    }
    public void LoseLife()
    {
        loseLife = Resources.Load<AudioClip>("Lose Life");
        soundAud.PlayOneShot(loseLife);
    }
    public void ButtonClick()
    {
        buttonClick = Resources.Load<AudioClip>("Button Click");
        soundAud.PlayOneShot(buttonClick);
    }
    public void SoundVolumeChange()
    {
        soundAud.volume = soundSlider.value;
    }
    public void MusicVolumeChange()
    {
        musicAud.volume = musicSlider.value;
    }
    public void SoundEnabler()
    {
        soundEnabled = !soundEnabled;
        soundAud.mute = !soundEnabled;
        if (soundEnabled) soundEnabledButton.GetComponent<Image>().sprite = soundEnabledSprite;
        else soundEnabledButton.GetComponent<Image>().sprite = soundDisabledSprite;
    }
    public void MusicEnabler()
    {
        musicEnabled = !musicEnabled;
        musicAud.mute = !musicEnabled;
        if (musicEnabled) musicEnabledButton.GetComponent<Image>().sprite = musicEnabledSprite;
        else musicEnabledButton.GetComponent<Image>().sprite = musicDisabledSprite;
    }

    public void LoadEnabledSounds()
    {
        if (soundEnabled)
        {
            soundEnabledButton.GetComponent<Image>().sprite = soundEnabledSprite;
            soundAud.mute = false;
        }
        else
        {
            soundEnabledButton.GetComponent<Image>().sprite = soundDisabledSprite;
            soundAud.mute = true;
        }

        if (musicEnabled)
        {
            musicEnabledButton.GetComponent<Image>().sprite = musicEnabledSprite;
            musicAud.mute = false;
        }
        else
        {
            musicEnabledButton.GetComponent<Image>().sprite = musicDisabledSprite;
            musicAud.mute = true;
        }
    }
    public void PlayMenuMusic()
    {
        musicAud.Stop();
        menuMusic = Resources.Load<AudioClip>("Menu Music");
        musicAud.clip = menuMusic;
        musicAud.Play();
    }
    public void PlayInGameMusic()
    {
        musicAud.Stop();
        inGameMusic = Resources.Load<AudioClip>("In Game Music");
        musicAud.clip = inGameMusic;
        musicAud.Play();
    }
    public void RegenerateLife()
    {
        regeneration = Resources.Load<AudioClip>("Regeneration");
        soundAud.PlayOneShot(regeneration);
    }
}
