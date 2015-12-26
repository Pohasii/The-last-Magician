using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    Transform myTransform;
    public static bool BossResp;
    public GameObject EnemyBoss, EnemyCreep;

    void OnEnable()
    {
        GameController.NextWave += NewCreepWave;
    }
    void OnDisable()
    {
        GameController.NextWave += NewCreepWave;
    }

    void Start()
    {
        BossResp = false;
        myTransform = GetComponent<Transform>();
        GameController.EnemySpawnPoints.Add(gameObject);
        NewCreepWave();
    }

    void Update()
    {
        if (GameController.TotalEnemyCount >= 8)
        {
            CancelInvoke("SpawnCreeps");
        }
    }

    public void NewCreepWave()
    {
        InvokeRepeating("SpawnCreeps", 0, 5);
        CharacterUIController.SetTextTrigger(GameController.CreepWave.ToString(), Color.red);
    }

    public void SpawnCreeps()
    {
        GameObject creep = (GameObject)Instantiate(EnemyCreep, myTransform.position, Quaternion.identity);
        creep.name = "Creep";
        GameController.CurCreepCount++;
        GameController.TotalEnemyCount++;
    }

    public void SpawnBoss()
    {
        GameObject boss = (GameObject)Instantiate(EnemyBoss, myTransform.position, Quaternion.identity);
        boss.name = "Boss";
        GameController.CurBossCount++;
        GameController.TotalEnemyCount++;
        BossResp = true;
    }
}
