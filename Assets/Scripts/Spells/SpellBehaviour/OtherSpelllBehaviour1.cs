using UnityEngine;
using System.Collections;

public class OtherSpelllBehaviour1 : MonoBehaviour
{
    Transform myTransform;
    ParticleSystem PS;
    public Spell spell;

	void Start () 
    {
        myTransform = GetComponent<Transform>();
        PS = GetComponent<ParticleSystem>();
        myTransform.parent = PlayerScript.playerScript.GetComponent<Transform>();
        Destroy(gameObject, PS.duration);
        SpellCast();
	}

    void SpellCast()
    {
        Collider[] colliders = Physics.OverlapSphere(myTransform.position, spell.Radius, LayerMask.GetMask("Character"));

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].isTrigger)
                continue;
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce(spell.ExplosionForce1, myTransform.position, spell.Radius);
        }
    }

    public void SpellSetap(Spell newSpell)
    {
        spell = newSpell;
    }
}
