using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int highscore;

    public float soundVolume;
    public float musicVolume;

    public bool soundEnabled;
    public bool musicEnabled;

    public string[] soundsettings = new string[4];

    public PlayerData(Game game)
    {
        highscore = game.highscore;

        soundsettings[0] = game.soundVolume.ToString();
        soundsettings[1] = game.musicVolume.ToString();
        soundsettings[2] = game.soundEnabled.ToString();
        soundsettings[3] = game.musicEnabled.ToString();
    }
}
