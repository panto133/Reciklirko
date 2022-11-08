using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreenLogic : MonoBehaviour
{
    public string[] hints;
    public TextMeshProUGUI text;

    private int prev = -1;

    private void OnEnable()
    {
        int rand;

        do
        {
            rand = Random.Range(0, hints.Length);
        } while (rand == prev);

        prev = rand;
        text.text = hints[rand];

    }
}
