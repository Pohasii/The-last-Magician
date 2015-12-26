using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController controller;

    public static int CurCreepCount, CurBossCount,TotalEnemyCount;
    public static List<GameObject> EnemySpawnPoints = new List<GameObject>();
    public static List<Spell> PlayerSpells = new List<Spell>();
    public static int RuneCount;
    public static int CreepWave = 1;

    public delegate void d_NewxWave();
    public static event d_NewxWave NextWave;

    public GameObject ScoreRuneObj;

    public static void RespNewEnemy(string p_name)
    {
        if (p_name == "Creep")
            CurCreepCount--;
        else
            CurBossCount--;

        if (TotalEnemyCount < 20)
        {
            if (CurCreepCount < TotalEnemyCount)
            {
                EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Count)].SendMessage("SpawnCreeps");
            }
        }
        else
        {
            if (!EnemySpawn.BossResp && CurCreepCount <= 4 && p_name == "Creep")
            {
                EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Count)].SendMessage("SpawnBoss");
            }
            else if (CurCreepCount == 0 && CurBossCount == 0)
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

        Enemy.ScoreRune = ScoreRuneObj;

        RuneCount = 1200;
    }
}
