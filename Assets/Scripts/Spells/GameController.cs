using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController controller;

    public static List<Spell> PlayerSpells = new List<Spell>();
    public static int RuneCount;

    public GameObject ScoreRuneObj;

    void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else
            if (controller != this)
            {
                Destroy(gameObject);
            }

        RuneCount = 1200;
    }
}
