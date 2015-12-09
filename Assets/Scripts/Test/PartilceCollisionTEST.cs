using UnityEngine;
using System.Collections;

public class PartilceCollisionTEST : MonoBehaviour
{
    SphereCollider coll;

    void Start()
    {
        coll = GetComponent<SphereCollider>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        GetComponent<ParticleSystem>().Play();
    }

    void OnParticleCollision(GameObject go)
    {
        Debug.Log("asdasds");
        if (go.tag == "Player")
        {
            Debug.Log("asdasds");
        }
    }
}
