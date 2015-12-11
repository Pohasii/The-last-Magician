using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpellSDataBase : MonoBehaviour
{
    public static List<Spell> Spells = new List<Spell>();

    public static List<Element> ElementsInDB = new List<Element>();
    public static Element FireElement, FrostElement, ArcaneElement;

    public List<Material> ElementMaterial = new List<Material>();
    public Material LineMaterial;
    public GameObject pointObj;

    ////Массивы элементов для спела
    List<Element> FireBallElements = new List<Element>();
    List<Element> FrostBoltElements = new List<Element>();
    List<Element> BlinkElements = new List<Element>();
    List<Element> FireAreaElements = new List<Element>();
    List<Element> BombElements = new List<Element>();
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

        FireElement = new Element("Fire", "Кароче это огонь", 10, new Vector3(50, 0, -20), ElementMaterial[0], Color.red);
        FrostElement = new Element("Frost", "Кароче это лед", 10, new Vector3(100, 0, -20), ElementMaterial[1], Color.blue);
        ArcaneElement = new Element("Arcane", "Кароче это тайная магия", 10, new Vector3(150, 0, -20), ElementMaterial[2], Color.cyan);

        ElementsInDB.Add(FireElement);
        ElementsInDB.Add(FrostElement);
        ElementsInDB.Add(ArcaneElement);

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
        //////
        BombElements.Add(FireElement);
        BombElements.Add(FrostElement);
        BombElements.Add(FrostElement);
        /////////////////////////////////////////

        Spells.Add(new SWNP("FireBall", "p_Damage", pointObj, FireBallElements, 25f, 2f, 2f, DamageSpell.SpellDamageTypes.Fire));
        Spells.Add(new DamageSpell("FrostBolt", "Slow", pointObj, FrostBoltElements, 1f, 1f, 2f, DamageSpell.SpellDamageTypes.Frost));
        Spells.Add(new SWOP("Fire area", "Pian", pointObj, FireAreaElements, 10f * Time.deltaTime, 1f, 20f, DamageSpell.SpellDamageTypes.Fire));
        Spells.Add(new SWOP("Bomb", "BOOM", pointObj, BombElements, 5, 35, 2, 10, DamageSpell.SpellDamageTypes.Fire));
        Spells.Add(new MovementSpell("Blink", "Blink111", BlinkElements, 0f, 0, 15f));
    }
}
