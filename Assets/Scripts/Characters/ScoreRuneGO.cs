using UnityEngine;
using System.Collections;

public class ScoreRuneGO : MonoBehaviour
{
    int score = 10;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            GameController.RuneCount += score;
            Destroy(gameObject);
        }
    }
}
