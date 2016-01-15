using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    Transform myTransform;
    Rigidbody myRigidbody;
    Collider myCollider;

    public Enemy enemy;

    public GameObject slider;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        myTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (enemy.IsDead)
            return;

        enemy.Move();
        enemy.AttacTrigger();

        EnemyDeath();
    }

    void EnemyDeath()
    {
        if (enemy.CurHP1 <= 0 && !enemy.IsDead)
        {
            myRigidbody.isKinematic = true;
            myCollider.enabled = false;

            Instantiate(GameController.controller.ScoreRuneObj, myTransform.position + Vector3.up, Quaternion.identity);
            EnemySpawn.enemySpawn.RespNewEnemy(name);

            enemy.IsDead = true;
            enemy.anim.SetTrigger("Dead");
            enemy.EnemyNav.enabled = false;
            enemy.hpSlider.gameObject.SetActive(false);

            Destroy(gameObject, 3);
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
