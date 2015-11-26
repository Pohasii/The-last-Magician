using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Spell
{
    private string SpellName;
    public string SpellName1
    {
        get { return SpellName; }
        set { SpellName = value; }
    }

    private string SpellDescription;
    public string SpellDescription1
    {
        get { return SpellDescription; }
        set { SpellDescription = value; }
    }

    protected float SpellManaCost;
    protected float SpellCastTime;
    protected float SpellCooldown;
    protected SpellsEffects SpellEffect;
    public enum SpellsEffects { None, DoT, Slowdown, Root }

    Vector3 StartPos;
    public Vector3 StartPos1
    {
        get { return StartPos; }
        set { StartPos = value; }
    }

    private Color color;
    public Color Color1
    {
        get { return color; }
        set { color = value; }
    }

    Material material;
    public Material Material1
    {
        get { return material; }
        set { material = value; }
    }

    private List<Element> ComponentsOfSpell;
    public List<Element> ComponentsOfSpell1
    {
        get { return ComponentsOfSpell; }
        set { ComponentsOfSpell = value; }
    }

    public Spell()
    { }

    public virtual void SpellCast(Transform SpawnPos, Transform cameraTransform)
    {

    }

    public Spell(Color p_Color, Material p_Material, string Name, string Description, List<Element> Components, float ManaCost, float CastTime, float Cooldown)
    {
        material = p_Material;
        color = p_Color;
        SpellName = Name;
        SpellDescription = Description;
        ComponentsOfSpell = Components;
        SpellManaCost = ManaCost;
        SpellCastTime = CastTime;
        SpellCooldown = Cooldown;
    }
}

[System.Serializable]
public class DamageSpell : Spell
{
    float SpellDamage;
    public float SpellDamage1
    {
        get { return SpellDamage; }
        set { SpellDamage = value; }
    }
    SpellDamageTypes SpellDamageType;
    public enum SpellDamageTypes { Fire, Frost }
    GameObject SpellObj;
    public GameObject SpellObj1
    {
        get { return SpellObj; }
        set { SpellObj = value; }
    }

    public DamageSpell(GameObject p_SpellObj, Material p_Material, Color p_Color, string Name, string Description, List<Element> Components, float Cooldown, float Damage, float CastTime, float Manacost, SpellDamageTypes DamageType, SpellsEffects Effect)
        : base(p_Color, p_Material, Name, Description, Components, Manacost, CastTime, Cooldown)
    {
        SpellDamage = Damage;
        SpellObj = p_SpellObj;
        SpellDamageType = DamageType;
        SpellEffect = Effect;
    }

    public override void SpellCast(Transform SpawnPos, Transform cameraTransform)
    {
        base.SpellCast(SpawnPos, cameraTransform);
        GameObject ob = (GameObject)GameObject.Instantiate(SpellObj, SpawnPos.position, cameraTransform.rotation);
        ob.GetComponent<SpellBehaviour>().spell = this;
    }
}

[System.Serializable]
public class MovementSpell : Spell
{
    float SpellTeleportDistance;
    LayerMask BarrierMask = LayerMask.GetMask("Barrier");
    LayerMask FloorMask = LayerMask.GetMask("Floor");

    public MovementSpell(Color p_Color, Material p_Material, string Name, string Description, List<Element> Components, float Cooldown, float CastTime, float TeleportDistance, float Manacost)
        : base(p_Color, p_Material, Name, Description, Components, Manacost, CastTime, Cooldown)
    {
        SpellName1 = Name;
        SpellDescription1 = Description;
        SpellTeleportDistance = TeleportDistance;
        SpellManaCost = Manacost;
    }

    public override void SpellCast(Transform myTransform, Transform cameraTransform)
    {
        base.SpellCast(myTransform, cameraTransform);
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, SpellTeleportDistance, FloorMask))
        {
            myTransform.position = new Vector3(hit.point.x, myTransform.position.y, hit.point.z);
        }
        else
        {
            Collider[] coll = Physics.OverlapSphere(new Vector3(cameraTransform.position.x, cameraTransform.position.y, cameraTransform.position.z + SpellTeleportDistance), 2, BarrierMask);
            if (coll.Length == 0)
            {
                if (!Physics.Raycast(ray, out hit, Mathf.Infinity, FloorMask))
                {
                    myTransform.position += cameraTransform.forward * SpellTeleportDistance;
                }
                else
                {
                    myTransform.position += myTransform.forward * SpellTeleportDistance;
                }
            }
            else
                if (Physics.Raycast(ray, out hit, SpellTeleportDistance, BarrierMask))
                myTransform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - myTransform.localScale.z / 2);
        }
    }
}
