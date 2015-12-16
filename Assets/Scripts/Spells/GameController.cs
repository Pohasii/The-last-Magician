using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static int CurEnemyCount, TotalEnemyCount;
    public static List<GameObject> EnemySpawnPoints = new List<GameObject>();
    public static List<Spell> PlayerSpells = new List<Spell>();
    public static int RuneCount;
    public static int CreepWave;

    public delegate void MovePlayer();
    public static event MovePlayer NextWave; 

    public static void RespNewEnemy(string p_name)
    {
        if(p_name == "Creep")
        CurEnemyCount--;

        if (TotalEnemyCount < 20)
        {
            if (CurEnemyCount < TotalEnemyCount)
            {
                EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Count)].SendMessage("SpawnCreeps");
            }
        }
        else
        {
            if (CurEnemyCount == 4)
            {
                EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Count)].SendMessage("SpawnBoss");
            }
            else if (CurEnemyCount == 0)
            {
                CreepWave++;
                TotalEnemyCount = 0;

                if (NextWave != null)
                    NextWave();
                //Time.timeScale = 0;
            }
        }
    }

    void Awake()
    {
        RuneCount = 120;
        DontDestroyOnLoad(this);
    }
}
