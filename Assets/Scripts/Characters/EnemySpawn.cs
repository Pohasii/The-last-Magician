using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn enemySpawn;

    public int CurCreepCount, CurBossCount, CurTotalEnemyCount;

    [Space(20)]
    public bool BossResp;
    public GameObject EnemyBoss, EnemyCreep;

    public GameObject EndOfCreepWave;

    public List<Transform> EnemySpawnPoints = new List<Transform>();

    [Tooltip("Спавнить босса сразу?")]
    [Space(20)]
    public bool spawnBoss;

    [Tooltip("Максимальное количество крипов в одной волне")]
    [Space(20)]
    public int maxEnemyCount;

    [Tooltip("При каком количестве живых мобов спавнить босса")]
    public int WhenBossResp;

    [Tooltip("Начальное количество мобов")]
    public int startEnemyCount;

    [Tooltip("Сколько мобов будет спавниться за раз")]
    public int burstEnemyCount;

    [Tooltip("Задержка спавна мобов")]
    public float delayBetweenBurst;

    public static int CreepWave = 1;

    void Awake()
    {
        enemySpawn = this;

        if (startEnemyCount > maxEnemyCount)
            startEnemyCount = maxEnemyCount;

        if (burstEnemyCount > startEnemyCount)
            burstEnemyCount = startEnemyCount;
    }

    void Start()
    {
        for (int i = 0; i < CreepWave - 1; i++)
        {
            PlayerScript.playerScript.player.StateByWave();
            CharactersDB.characterDB.StateByWave();
            maxEnemyCount++;
            startEnemyCount++;
            burstEnemyCount++;
        }

        BossResp = false;
        NewCreepWave();
    }

    void Update()
    {
        if (CurTotalEnemyCount >= startEnemyCount)
        {
            CancelInvoke("SpawnCreeps");
        }
    }

    public void RespNewEnemy(string p_name)
    {
        if (p_name == "Creep")
            CurCreepCount--;
        else
            CurBossCount--;

        if (CurTotalEnemyCount < maxEnemyCount)
        {
            if (CurCreepCount < CurTotalEnemyCount)
            {
                SpawnCreeps();
            }
        }
        else
        {
            if (!spawnBoss && !BossResp && CurCreepCount <= WhenBossResp && p_name == "Creep")
            {
                SpawnBoss();
            }
            else if (CurCreepCount == 0 && CurBossCount == 0)
            {
                EndOfCreepWave.SetActive(true);
            }
        }
    }

    public void NewCreepWave()
    {
        if (spawnBoss)
            SpawnBoss();

        for (int i = 0; i < burstEnemyCount; i++)
            InvokeRepeating("SpawnCreeps", 0, delayBetweenBurst);

        BossResp = false;
        CharacterUIController.SetTextTrigger(CreepWave.ToString(), Color.red, 50);

        maxEnemyCount++;
        startEnemyCount++;
        burstEnemyCount++;
    }

    public void SpawnCreeps()
    {
        GameObject creep = (GameObject)Instantiate(EnemyCreep, EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Count)].position, Quaternion.identity);

        CharactersDB.characterDB.SetCreepConstructor(creep.transform);
        creep.GetComponent<EnemyScript>().enemy = CharactersDB.characterDB.creep;

        creep.name = "Creep";

        CurCreepCount++;
        CurTotalEnemyCount++;
    }

    public void SpawnBoss()
    {
        GameObject boss = (GameObject)Instantiate(EnemyBoss, EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Count)].position, Quaternion.identity);

        CharactersDB.characterDB.SetBossConstructor(boss.transform);
        boss.GetComponent<EnemyScript>().enemy = CharactersDB.characterDB.boss;

        boss.name = "Boss";

        CurBossCount++;
        CurTotalEnemyCount++;

        BossResp = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (EndOfCreepWave.activeSelf && other.tag == "Player")
        {
            PlayerScript.playerScript.player.StateByWave();
            CharactersDB.characterDB.StateByWave();

            CreepWave++;
            CurTotalEnemyCount = 0;

            NewCreepWave();

            StartCoroutine(LevelManager.levelManager.LoadLevelWithFade(LevelManager.levelManager.LoadLevelFromGame, "Menu"));
        }
    }
}