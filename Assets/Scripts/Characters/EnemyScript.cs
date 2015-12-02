using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    Transform myTransform;
    NavMeshAgent Nav;
    public float AttacDelay;
    public float Damage;
    public float HP;
    public float MP;
    public float MoveSpeed;
    public float HPRegen;
    public float MPRegen;

    public Canvas canvas;
    public GameObject ScoreRune;

    public Enemy enemy;

    void Start()
    {
        Enemy.ScoreRune = ScoreRune;
        myTransform = GetComponent<Transform>();
        Nav = GetComponent<NavMeshAgent>();
        enemy = new Enemy(Nav, canvas, Damage, AttacDelay, HP, MP, HPRegen, MPRegen);
    }

    void Update()
    {
        enemy.Move(myTransform);
        enemy.AttacTrigger();

        if (enemy.IsDead1)
        {
            if (name == "Creep")
            {
                GameController.CurEnemyCount--;
            }
            if (GameController.TotalEnemyCount < 20)
            {
                if (GameController.CurEnemyCount < GameController.TotalEnemyCount)
                {
                    GameController.EnemySpawnPoints[Random.Range(0, GameController.EnemySpawnPoints.Count)].SendMessage("SpawnCreeps");
                }
            }
            else
            {
                if (GameController.CurEnemyCount == 4)
                {
                    GameController.EnemySpawnPoints[Random.Range(0, GameController.EnemySpawnPoints.Count)].SendMessage("SpawnBoss");
                }
                else if (GameController.CurEnemyCount == 0)
                {
                    //Time.timeScale = 0;
                }
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            enemy.PlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            enemy.PlayerInRange = false;
        }
    }
}
