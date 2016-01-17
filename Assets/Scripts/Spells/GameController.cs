using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController controller;

    public static List<Spell> PlayerSpells = new List<Spell>();
    public static List<Element> Elements = new List<Element>();

    public static int RuneCount = 1200;

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
    }
}