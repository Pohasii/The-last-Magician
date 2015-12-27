using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Character
{
    public float Damage { get; set; }
    protected float MaxHP;
    public float CurHP{get;set;}
    public float HPRegen { get; set; }
    public float MoveSpeed { get; set; }

    protected Slider hpSlider;

    public Character(float p_Dmg, float p_MaxHP, float p_HPRegen, float p_MoveSpeed)
    {
        Damage = p_Dmg;
        MaxHP = p_MaxHP;
        CurHP = p_MaxHP;
        HPRegen = p_HPRegen;
        MoveSpeed = p_MoveSpeed;
    }

    public virtual void Attac()
    {
 
    }

    public virtual void TakeDamage(HZSpell SpellInfo)
    {
        CurHP -= SpellInfo.SpellDamage1;
        if(hpSlider)
        hpSlider.value = CurHP;
    }
}
