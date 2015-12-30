using UnityEngine;
using System.Collections;

public class VoidZoneSpellBehaviour : MonoBehaviour
{
    public Spell spell;
    Collider myCollider;
    ParticleSystem PS;
    AudioSource AS;

    void Start()
    {
        Destroy(gameObject, spell.SpellLifeTime1);
    }

    void OnTriggerStay(Collider col)
    {
        PlayerScript playerScript;
        EnemyScript enemyScript;
      
        playerScript = col.GetComponent<PlayerScript>();
        enemyScript = col.GetComponent<EnemyScript>();

        if (enemyScript)
            enemyScript.enemy.TakeDamage((HZSpell)spell);
        if (playerScript)
            playerScript.player.TakeDamage((HZSpell)spell);
    }

    public void SpellSetap(Spell newSpell)
    {
        spell = newSpell;
    }
}
