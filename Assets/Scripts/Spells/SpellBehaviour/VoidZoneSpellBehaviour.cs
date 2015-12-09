using UnityEngine;
using System.Collections;

public class VoidZoneSpellBehaviour : MonoBehaviour
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

    void OnTriggerStay(Collider col)
    {
        PlayerScript playerScript;
        EnemyScript enemyScript;
      
        playerScript = col.GetComponent<PlayerScript>();
        enemyScript = col.GetComponent<EnemyScript>();

        if (enemyScript)
            enemyScript.enemy.TakeDamage((DamageSpell)spell);
        if (playerScript)
            playerScript.player.TakeDamage((DamageSpell)spell);
    }

    public void SpellSetap(Spell newSpell)
    {
        spell = newSpell;
    }
}
