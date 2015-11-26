using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    Transform myTransform;

    public GameObject Enemy;

    public float SpawnDelay;
    float timer;

    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= SpawnDelay)
        {
            Instantiate(Enemy, myTransform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
