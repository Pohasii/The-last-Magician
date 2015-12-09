using UnityEngine;
using System.Collections;

public class OtherSpellBehaviour : MonoBehaviour
{
    public Spell spell;
    Transform myTransform;
    Collider myCollider;
    ParticleSystem PS;
    AudioSource AS;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myCollider = GetComponent<Collider>();

        PS = GetComponentInChildren<ParticleSystem>();
        AS = GetComponent<AudioSource>();
        Destroy(gameObject, spell.SpellLifeTime1);
    }

    void OtherSpell(Collider col)
    {
        myCollider.enabled = false;
        AS.Play();
        PS.Play();
        Destroy(gameObject, PS.duration * 2);

        Collider[] colliders = Physics.OverlapSphere(myTransform.position, ((SWOP)spell).Radius, LayerMask.GetMask("Character"));

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].isTrigger)
                continue;
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce(1000, myTransform.position, ((SWOP)spell).Radius);

            EnemyScript enemyScript = targetRigidbody.GetComponent<EnemyScript>();
            PlayerScript playerScript = targetRigidbody.GetComponent<PlayerScript>();

            if (!enemyScript && !playerScript)
                continue;
            if (enemyScript)
                enemyScript.enemy.TakeDamage((DamageSpell)spell);
            if (playerScript)
                playerScript.player.TakeDamage((DamageSpell)spell);
        }

    }

    void OnTriggerEnter(Collider col)
    {
        OtherSpell(col);
    }

    public void SpellSetap(Spell newSpell)
    {
        spell = newSpell;
    }
}
