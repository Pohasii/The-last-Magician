using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    Transform myTransform;
    Rigidbody myRigidbody;
    Collider myCollider;
    NavMeshAgent Nav;
    public float AttacDelay;
    public float Damage;
    public float HP;
    public float MP;
    public float MoveSpeed;
    public float HPRegen;
    public float MPRegen;

    public Canvas canvas;

    public Enemy enemy;
    public Animator anim;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        myTransform = GetComponent<Transform>();
        Nav = GetComponent<NavMeshAgent>();
        StateByWave();
        enemy = new Enemy(anim, Nav, canvas, Damage, AttacDelay, HP, MP, HPRegen, MPRegen);
    }

    void Update()
    {
        if (!enemy.IsDead)
        {
            enemy.Move(myTransform);
            enemy.AttacTrigger();
        }

        enemy.Dead(myTransform, name);
    }

    void StateByWave()
    {
        HP *= 1.5f;
        Nav.speed += GameController.CreepWave;
        Damage += GameController.CreepWave + (10 * (GameController.CreepWave -1));
    }

    public void DisableComponents()
    {
        myRigidbody.isKinematic = true;
        myCollider.enabled = false;
        Nav.enabled = false;
        anim.SetBool("Attac", false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (!enemy.IsDead && col.tag == "Player")
        {
            anim.SetBool("Attac", true);
            enemy.PlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            anim.SetBool("Attac", false);
            enemy.PlayerInRange = false;
        }
    }
}
