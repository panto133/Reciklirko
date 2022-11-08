using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLogic : MonoBehaviour
{
    public Sprite[] tree;
    public Sprite[] ice;

    public GameObject menuGraphics;
    public GameObject optionsGraphics;
    public GameObject gameGraphics;
    public GameObject chooseDifficultyGraphics;
    public GameObject inGameGraphics;
    public GameObject deathScreenGraphics;
    public GameObject pausedGraphics;
    public GameObject controlsGraphics;

    public GameObject menuBackground;
    public GameObject deathScreenBackground;
    public GameObject optionsBackground;
    public GameObject controlsBackground;
    public GameObject gameBackground;
    public GameObject inGameBackground;

    public GameObject treeButton;
    public GameObject iceButton;

    public bool treeChosen = true;

    public Sprite treeDefaultGraphics;
    public Sprite treeSelectedGraphics;
    public Sprite iceDefaultGraphics;
    public Sprite iceSelectedGraphics;

    public AudioManager soundMan;
    public SpeedManager speedMan;
    public Game game;

    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI scoreText;

    public void TreeMapChosen()
    {
        treeChosen = true;

        treeButton.GetComponent<Image>().sprite = treeSelectedGraphics;
        iceButton.GetComponent<Image>().sprite = iceDefaultGraphics;

        menuBackground.GetComponent<Image>().sprite = tree[0];
        deathScreenBackground.GetComponent<Image>().sprite = tree[4];
        optionsBackground.GetComponent<Image>().sprite = tree[0];
        controlsBackground.GetComponent<Image>().sprite = tree[0];
        gameBackground.GetComponent<Image>().sprite = tree[0];
        inGameBackground.GetComponent<Image>().sprite = tree[1];

        game.currentBackgrounds[0] = tree[1];
        game.currentBackgrounds[1] = tree[2];
        game.currentBackgrounds[2] = tree[3];
        game.currBackgroundIndex = 0;
    }

    public void IceMapChosen()
    {
        treeChosen = false;

        iceButton.GetComponent<Image>().sprite = iceSelectedGraphics;
        treeButton.GetComponent<Image>().sprite = treeDefaultGraphics;

        menuBackground.GetComponent<Image>().sprite = ice[0];
        deathScreenBackground.GetComponent<Image>().sprite = ice[4];
        optionsBackground.GetComponent<Image>().sprite = ice[0];
        controlsBackground.GetComponent<Image>().sprite = ice[0];
        gameBackground.GetComponent<Image>().sprite = ice[0];
        inGameBackground.GetComponent<Image>().sprite = ice[1];

        game.currentBackgrounds[0] = ice[1];
        game.currentBackgrounds[1] = ice[2];
        game.currentBackgrounds[2] = ice[3];
        game.currBackgroundIndex = 0;
    }

    public void FromChooseDifficultyToMenu()
    {
        chooseDifficultyGraphics.SetActive(false);
        gameGraphics.SetActive(false);
        menuGraphics.SetActive(true);
    }
    public void FromChooseDifficultyToGame()
    {
        chooseDifficultyGraphics.SetActive(false);
        menuGraphics.SetActive(false);
        inGameGraphics.SetActive(true);
        game.ResetGame();
        speedMan.ResetSpeed();
        soundMan.PlayInGameMusic();
        game.started = true;
    }
    public void FromMenuToChooseDifficulty()
    {
        menuGraphics.SetActive(false);
        gameGraphics.SetActive(true);
        chooseDifficultyGraphics.SetActive(true);
    }
    public void FromGameToDeathScreen(int score, int highscore)
    {
        soundMan.PlayMenuMusic();
        gameGraphics.SetActive(false);
        deathScreenGraphics.SetActive(true);
        highscoreText.text = "NAJBOLJI REZULTAT: " + highscore;
        scoreText.text = "REZULTAT: " + score;
    }
    public void FromGameToMenu()
    {
        Time.timeScale = 1;
        game.started = false;
        speedMan.ResetSpeed();
        game.ResetGame();
        gameGraphics.SetActive(false);
        pausedGraphics.SetActive(false);
        menuGraphics.SetActive(true);
    }
    public void FromDeathScreenToMenu()
    {
        deathScreenGraphics.SetActive(false);
        menuGraphics.SetActive(true);
        soundMan.PlayMenuMusic();
    }
    public void FromDeathScreenToGame()
    {
        soundMan.PlayInGameMusic();
        game.started = true;
        deathScreenGraphics.SetActive(false);
        gameGraphics.SetActive(true);
        game.ResetGame();
        speedMan.ResetSpeed();
    }
    public void FromMenuToOptions()
    {
        menuGraphics.SetActive(false);
        optionsGraphics.SetActive(true);
    }
    public void FromOptionsToMenu()
    {
        optionsGraphics.SetActive(false);
        menuGraphics.SetActive(true);
    }
    public void FromMenuToControls()
    {
        menuGraphics.SetActive(false);
        controlsGraphics.SetActive(true);
    }
    public void FromControlsToMenu()
    {
        controlsGraphics.SetActive(false);
        menuGraphics.SetActive(true);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pausedGraphics.SetActive(true);
    }
    public void Play()
    {
        Time.timeScale = 1;
        pausedGraphics.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    private void Awake()
    {
        TreeMapChosen();
    }
}
