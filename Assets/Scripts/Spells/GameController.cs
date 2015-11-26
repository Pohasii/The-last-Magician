using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static List<Spell> PlayerSpells = new List<Spell>();
    public static int RuneCount;

    void Awake()
    {
        RuneCount = 120;
        DontDestroyOnLoad(this);
    }
}
