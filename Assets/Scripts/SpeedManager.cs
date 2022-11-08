using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedManager : MonoBehaviour
{
    private float defaultSpeed;
    public Game game;

    public float Speed { get; private set; } = 0f;
    private void FixedUpdate()
    {
        Speed += Time.deltaTime / 30f;
        Speed = Mathf.Clamp(Speed, defaultSpeed, 20f);       
    }
    public void ResetSpeed()
    {
        Speed = defaultSpeed;       
    }
    public void Slow()
    {
        defaultSpeed = 4f;
        game.chosenTimer = 1.5f;
    }
    public void Normal()
    {
        defaultSpeed = 5.5f;
        game.chosenTimer = 1.2f;
    }
    public void Fast()
    {
        defaultSpeed = 7.5f;
        game.chosenTimer = 1f;
    }
}
