using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharactersDB : MonoBehaviour 
{
    public static CharactersDB characterDB;

    public float Damage;
    public float HP;
    public float HPRegen;
    public float MoveSpeed;

    void Awake()
    {
        characterDB = this;
    }

    public void SetStatsText(List<Text> Stats)
    {
        Stats[0].text = "Damage: " + Damage.ToString();
        Stats[1].text = "Health: " + HP.ToString();
        Stats[2].text = "HP/S: " + HPRegen.ToString();
        Stats[3].text = "Move speed: " + MoveSpeed.ToString();
    }
}
