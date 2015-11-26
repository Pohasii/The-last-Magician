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
        myTransform.Translate(Vector3.forward * 2, Space.Self);
        Destroy(this.gameObject, 2);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy" && !col.isTrigger)
        {
            col.GetComponent<EnemyScript>().enemy.TakeDamage(spell.SpellDamage1);
            Destroy(gameObject);
        }
    }
}
