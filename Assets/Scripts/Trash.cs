using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private Game game;

    private Vector3 target = new Vector3(0, -2.88f, 30f);

    private float speed;

    private bool positionSet = false;

    public string trashName;
    private string chosenContainer;

    private void Awake()
    {
        game = GameObject.Find("Game").GetComponent<Game>();
        speed = game.GetComponent<SpeedManager>().Speed;
    }

    private void FixedUpdate()
    {
        if(!positionSet)
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, (speed / 3f) * Time.deltaTime);
        else
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, 8f * Time.deltaTime);

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 30f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!positionSet)
        {
            game.RemoveTrash(gameObject);
            game.LoseLife();
            game.ChangeBackground();
        }
        else
        {
            if(trashName == chosenContainer)
            {
                game.AddScore();
                GameObject.Find("SoundManager").GetComponent<AudioManager>().Trash(trashName);
            }
            else if (game.GetComponent<Game>().started)
            {
                game.RemoveTrash(gameObject);
                game.LoseLife();
                game.ChangeBackground();
            }
        }
        game.RegenerationCounter();
        Destroy(gameObject);
    }

    public void TowardsPaper()
    {
        target = new Vector3(-1.72f, -2.88f, 30f);
        DestroyTrash(false);
        positionSet = true;
        chosenContainer = "Paper";
    }
    public void TowardsPlastic()
    {
        target = new Vector3(0, -2.88f, 30f);
        DestroyTrash(false);
        positionSet = true;
        chosenContainer = "Plastic";
    }
    public void TowardsGlass()
    {
        target = new Vector3(1.72f, -2.88f, 30f);
        DestroyTrash(false);
        positionSet = true;
        chosenContainer = "Glass";
    }
    private void DestroyTrash(bool succesfull)
    {
        if(succesfull)
            game.AddScore();

        game.RemoveTrash(gameObject);
    }
}
