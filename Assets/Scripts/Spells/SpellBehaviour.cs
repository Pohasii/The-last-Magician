using UnityEngine;
using System.Collections;

public class SpellBehaviour : MonoBehaviour
{
    Transform myTransform;
    public DamageSpell spell;

    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    void Update()
    {
        spell.SpellBehaviour(myTransform);
    }

    void OnTriggerEnter(Collider col)
    {
        if (spell.SBT == Spell.SpellBehaviourType.Single && col.tag == "Enemy" && !col.isTrigger)
        {
            col.GetComponent<EnemyScript>().enemy.TakeDamage(spell);
            myTransform.GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (spell.SBT == Spell.SpellBehaviourType.DOT && col.tag == "Enemy" && !col.isTrigger)
        {
            col.GetComponent<EnemyScript>().enemy.TakeDamage(spell);
        }
    }

    void OnParticleCollision(GameObject go)
    {
        if (go.tag == "Enemy")
        {
            go.GetComponent<EnemyScript>().enemy.TakeDamage(spell);
        }
    }
}
