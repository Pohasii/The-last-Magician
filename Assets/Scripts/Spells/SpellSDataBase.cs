using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpellSDataBase : MonoBehaviour
{
    public List<GameObject> SpellObj = new List<GameObject>();

    public static List<Spell> Spells = new List<Spell>();

    public static Element FireElement, FrostElement, ArcaneElement;

    public static List<Element> ElementsInDB = new List<Element>();

    public List<Material> SpellMaterial = new List<Material>();
    public List<Material> ElementMaterial = new List<Material>();
    public Material LineMaterial;
    public GameObject pointObj;

    ////Массивы элементов для спела
    List<Element> FireBallElements = new List<Element>();
    List<Element> FrostBoltElements = new List<Element>();
    List<Element> BlinkElements = new List<Element>();
    List<Element> FireAreaElements = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();
    //List<Element> ElementsOfFireBall = new List<Element>();

    void Awake()
    {
        SpellPoints.lineMaterial = LineMaterial;
        ElementsInDB.Add(new Element("Fire", "Кароче это огонь", 10, new Vector3(50, 0, -20), ElementMaterial[0], Color.red));
        ElementsInDB.Add(new Element("Frost", "Кароче это лед", 10, new Vector3(100, 0, -20), ElementMaterial[1], Color.blue));
        ElementsInDB.Add(new Element("Arcane", "Кароче это тайная магия", 10, new Vector3(150, 0, -20), ElementMaterial[2], Color.cyan));

        FireElement = ElementsInDB[0];
        FrostElement = ElementsInDB[1];
        ArcaneElement = ElementsInDB[2];

        /////////////////////////////////////////
        FireBallElements.Add(FireElement);
        FireBallElements.Add(ArcaneElement);
        FireBallElements.Add(FireElement);
        //////
        FrostBoltElements.Add(FrostElement);
        FrostBoltElements.Add(ArcaneElement);
        FrostBoltElements.Add(FireElement);
        //////
        BlinkElements.Add(ArcaneElement);
        BlinkElements.Add(ArcaneElement);
        BlinkElements.Add(ArcaneElement);
        //////
        FireAreaElements.Add(FireElement);
        FireAreaElements.Add(FireElement);
        FireAreaElements.Add(FrostElement);
        /////////////////////////////////////////

        Spells.Add(new DamageSpell(Spell.SpellCastType.NoPoints, Spell.SpellBehaviourType.Single, "FireBall", "Damage", SpellMaterial[0], pointObj, SpellObj[0], FireBallElements, 25f, 2f, 2f, DamageSpell.SpellDamageTypes.Fire));
        Spells.Add(new DamageSpell(Spell.SpellCastType.NoPoints, Spell.SpellBehaviourType.Single, "FrostBolt", "Slow", SpellMaterial[1], pointObj, SpellObj[1], FrostBoltElements, 1f, 1f, 2f, DamageSpell.SpellDamageTypes.Frost));
        Spells.Add(new DamageSpell(Spell.SpellCastType.OnePoint, Spell.SpellBehaviourType.DOT, "Fire area", "Pian", SpellMaterial[3], pointObj, SpellObj[2], FireAreaElements, 10f * Time.deltaTime, 2f, 20f, DamageSpell.SpellDamageTypes.Fire));
        Spells.Add(new MovementSpell(Spell.SpellCastType.NoPoints, Spell.SpellBehaviourType.Single, "Blink", "Blink111", SpellMaterial[2], BlinkElements, 0f, 0, 15f));
    }
}
