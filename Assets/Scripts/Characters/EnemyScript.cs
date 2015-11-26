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
            Destroy(gameObject);
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
