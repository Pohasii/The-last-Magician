using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    Transform myTransform;

    public GameObject EnemyBoss, EnemyCreep;

    public float SpawnDelay;
    float timer;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        GameController.EnemySpawnPoints.Add(gameObject);
        InvokeRepeating("SpawnCreeps", 0, 5);
    }

    void Update()
    {
        if (GameController.TotalEnemyCount >= 8)
        {
            CancelInvoke("SpawnCreeps");
        }
    }

    public void SpawnCreeps()
    {
        GameObject creep = (GameObject)Instantiate(EnemyCreep, myTransform.position, Quaternion.identity);
        creep.name = "Creep";
        GameController.CurEnemyCount++;
        GameController.TotalEnemyCount++;
    }

    public void SpawnBoss()
    {
        GameObject boss = (GameObject)Instantiate(EnemyBoss, myTransform.position, Quaternion.identity);
        boss.name = "Boss";
    }
}
