using UnityEngine;
using System.Collections;

public class SingleSpellBehaviour : MonoBehaviour
{
    public Spell spell;
    Transform myTransform;
    Collider myCollider;
    ParticleSystem PS;
    AudioSource AS;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        Destroy(gameObject, spell.SpellLifeTime1);
    }

    void Update()
    {
        myTransform.Translate(Vector3.forward * 30 * Time.deltaTime, Space.Self);
    }

    void SingleSpell(Collider col)
    {
        if (col.tag == "Enemy" && !col.isTrigger)
        {
            col.GetComponent<EnemyScript>().enemy.TakeDamage((HZSpell)spell);
            myTransform.GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        SingleSpell(col);
    }

    public void SpellSetap(Spell newSpell)
    {
        spell = newSpell;
    }
}
