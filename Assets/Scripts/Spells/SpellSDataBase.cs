using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpellSDataBase : MonoBehaviour
{
    public static List<Spell> Spells = new List<Spell>();
    public static SWNP Attac;

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
    List<Element> WaveElements = new List<Element>();
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
        if (Spells.Count > 0)
            return;

        SpellPoints.lineMaterial = LineMaterial;

        if (Application.loadedLevelName == "Menu")
        {
            FireElement = new Element(0, "Fire", "Кароче это огонь", 10, Menu.SMenu.FireElementPos.localPosition, ElementMaterial[0]);
            FrostElement = new Element(1, "Frost", "Кароче это лед", 10, Menu.SMenu.FrostElementPos.localPosition, ElementMaterial[1]);
            ArcaneElement = new Element(2, "Arcane", "Кароче это тайная магия", 10, Menu.SMenu.ArcaneElementPos.localPosition, ElementMaterial[2]);
        }
        else
            if (Application.loadedLevelName == "Game")
            {
                FireElement = new Element("Fire", "Кароче это огонь");
                FrostElement = new Element("Frost", "Кароче это лед");
                ArcaneElement = new Element("Arcane", "Кароче это тайная магия");
            }
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
        //////
        WaveElements.Add(ArcaneElement);
        WaveElements.Add(ArcaneElement);
        WaveElements.Add(FireElement);
        ///////////////////////////////////////// 

        Attac = new SWNP("Attac", "Base Attac", 5f, 0f, 0.3f, SpellDamageType.Fire);
        
        Spells.Add(new SWNP("FireBall", "over 9000 PlayerDamage", FireBallElements, 25f, 0f, 2f, SpellDamageType.Fire));
        //Spells.Add(new DamageSpell("FrostBolt", "Slow", pointObj, FrostBoltElements, 1f, 1f, 2f, SpellDamageType.Frost));
        Spells.Add(new SWOP("Fire area", "Pian", pointObj, FireAreaElements, 10f * Time.deltaTime, 0f, 20f, SpellDamageType.Fire));
        Spells.Add(new SWOP("Bomb", "BOOM", pointObj, BombElements, 7, 2000, 35, 0.5f, 10, SpellDamageType.Frost));
        Spells.Add(new SWNP("Wave", "PWWQWE", WaveElements, 0f, 20, 3000, 2f));
        Spells.Add(new MovementSpell("Blink", "Blink111", BlinkElements, 0f, 0, 15f));
    }
}
