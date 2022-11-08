using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public GameLogic gameLogic;

    public GameObject[] plastic;
    public GameObject[] glass;
    public GameObject[] paper;
    public GameObject[] livesPictures;

    private GameObject trash;
    public GameObject spawnedTrashGO;
    public GameObject border;
    public GameObject background;

    public AudioManager soundManager;

    public Sprite[] currentBackgrounds = new Sprite[3];

    private List<GameObject> spawnedTrash = new List<GameObject>();

    public Transform[] spawnpoints;
    public Transform parent;

    public Image flash;
    public Image flashRegeneration;

    public TextMeshProUGUI scoreText;

    public float timer;
    public float spawnTimer;
    public float chosenTimer;
    public float soundVolume = 1f;
    public float musicVolume = 1f;
    private float flashTimer = 1f;
    private float flashRegenerationTimer = 1f;

    public string[] soundsettings;

    public int highscore = 0;
    public int score = 0;

    private int lives = 3;
    private int prev = -1;
    public int currBackgroundIndex = 1;
    private int collectedTrash = 0;

    private bool isFlash = false;
    [HideInInspector]
    public bool started = false;
    public bool soundEnabled = true;
    public bool musicEnabled = true;
    private bool lifeLost = false;
    private bool lifeRegenerated = false;

    private void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            soundsettings[i] = "";
        }
        LoadPlayer();
        spawnTimer = chosenTimer;
    }

    private void Update()
    {     
        if(isFlash && flashTimer > 0)
        {
            flashTimer -= Time.deltaTime*2;
            flashTimer = Mathf.Clamp(flashTimer, 0, 1f);
            flash.color = new Color(255, 255, 255, flashTimer);
        }
        else
        {
            isFlash = false;
            flashTimer = 1f;
        }
        if(lifeRegenerated && flashRegenerationTimer > 0)
        {
            flashRegenerationTimer -= Time.deltaTime * 2;
            flashRegenerationTimer = Mathf.Clamp(flashRegenerationTimer, 0, 1f);
            flashRegeneration.color = new Color(255, 255, 255, flashRegenerationTimer);
        }
        else
        {
            lifeRegenerated = false;
            flashRegenerationTimer = 1f;
        }
    }

    private void FixedUpdate()
    {
        if (started)
        {
            timer += Time.deltaTime;
            spawnTimer -= Time.deltaTime / 175f;
            spawnTimer = Mathf.Clamp(spawnTimer, .5f, chosenTimer);
            if (timer >= spawnTimer)
            {
                timer = 0;
                GameObject clone = Instantiate(RandomGenerator(), spawnpoints[0].transform.localPosition, Quaternion.identity, parent) as GameObject;
                spawnedTrash.Add(clone);
            }
            if (spawnedTrash.Count > 0)
                trash = spawnedTrash[0];
            else
                trash = null;
        }
    }

    public void PaperButton()
    {
        if(trash != null)
            trash.GetComponent<Trash>().TowardsPaper();
    }
    public void GlassButton()
    {
        if (trash != null)
            trash.GetComponent<Trash>().TowardsGlass();
    }
    public void PlasticButton()
    {
        if (trash != null)
            trash.GetComponent<Trash>().TowardsPlastic();
    }
    public void RemoveTrash(GameObject item)
    {
        spawnedTrash.Remove(item);
    }
    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    public void GetSoundSettings()
    {
        AudioManager am = soundManager;
        soundVolume = am.soundAud.volume;
        musicVolume = am.musicAud.volume;
        soundEnabled = am.soundEnabled;
        musicEnabled = am.musicEnabled;
        SavePlayer();
    }
    public void LoseLife()
    {
        soundManager.LoseLife();
        collectedTrash--;
        lives--;
        livesPictures[lives].SetActive(false);
        if(lives == 2)
        {
            collectedTrash = 0;
        }
        lifeLost = true;
        flash.color = new Color(255f, 255f, 255f, 1f);
        isFlash = true;
        flashTimer = 1f;
        if (lives <= 0)
        {
            started = false;
            if (score > highscore) highscore = score;
            GameObject.Find("Game Logic").GetComponent<GameLogic>().FromGameToDeathScreen(score, highscore);
            ResetGame();
        }
    }
    public GameObject RandomGenerator()
    {
        int rand1, rand2;
        GameObject item;
        do
        {
            rand1 = Random.Range(0, 3);
            switch (rand1)
            {
                case 0: 
                    rand2 = Random.Range(0, plastic.Length);
                    item = plastic[rand2];
                    break;
                case 1:
                    rand2 = Random.Range(0, paper.Length);
                    item = paper[rand2];
                    break;
                default:
                    rand2 = Random.Range(0, glass.Length);
                    item = glass[rand2];
                    break;
            }
        } while (prev == rand1);
        prev = rand1;
        return item;
    }
    public void ResetGame()
    {
        SavePlayer();
        spawnedTrashGO.GetComponent<DeletingTrash>().Delete();
        foreach(GameObject life in livesPictures)
        {
            life.SetActive(true);
        }
        foreach(GameObject go in spawnedTrash)
        {
            Destroy(go);
        }
        score = 0;
        scoreText.text = "0";
        Destroy(trash);
        spawnedTrash.Clear();
        trash = null;
        currBackgroundIndex = 1;
        collectedTrash = 0;
        lifeLost = false;
        currBackgroundIndex = 0;
        if (gameLogic.treeChosen) gameLogic.TreeMapChosen();
        else gameLogic.IceMapChosen();
        GameObject.Find("Game").GetComponent<SpeedManager>().ResetSpeed();
        timer = 0;
        spawnTimer = chosenTimer;
        lives = 3;
    }
    public void ChangeBackground()
    {
        currBackgroundIndex++;
        if(currBackgroundIndex < 3)
            background.GetComponent<Image>().sprite = currentBackgrounds[currBackgroundIndex];
    }
    public void RegenerationCounter()
    {
        if(lifeLost)
        {
            collectedTrash++;
        }
        else
        {
            collectedTrash = 0;
        }
        if(collectedTrash > 30)
        {
            collectedTrash = 0;
            soundManager.RegenerateLife();
            currBackgroundIndex--;
            background.GetComponent<Image>().sprite = currentBackgrounds[currBackgroundIndex];
            lives++;
            lifeRegenerated = true;
            if(!livesPictures[1].active)
            {
                livesPictures[1].SetActive(true);
                return;
            }
            else
            {
                livesPictures[2].SetActive(true);
                lifeLost = false;
                return;
            }
        }
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            highscore = data.highscore;

            for (int i = 0; i < 4; i++)
            {
                soundsettings[i] = data.soundsettings[i];
            }
        }
    }
}
