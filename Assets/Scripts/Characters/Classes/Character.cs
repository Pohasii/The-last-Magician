using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Character
{
    public float Damage { get; set; }

    protected float MaxHP;
    [SerializeField]
    private float CurHP;
    public float CurHP1
    {
        get { return CurHP; }
        set { if (value > MaxHP) value = MaxHP; CurHP = value; hpSlider.value = value; }
    }

    private float HpRegen;
    public float HpRegen1
    {
        get { return HpRegen * Time.deltaTime; }
        set { HpRegen = value; }
    }

    public float MoveSpeed { get; set; }

    public Slider hpSlider;

    public Character(float p_Dmg, float p_MaxHP, float p_HPRegen, float p_MoveSpeed)
    {
        Damage = p_Dmg;
        MaxHP = p_MaxHP;
        CurHP = p_MaxHP;
        HpRegen = p_HPRegen;
        MoveSpeed = p_MoveSpeed;
    }

    public Character(float p_Dmg, float p_MaxHP, float p_MoveSpeed)
    {
        Damage = p_Dmg;
        MaxHP = p_MaxHP;
        CurHP = p_MaxHP;
        MoveSpeed = p_MoveSpeed;
    }

    public virtual void Attac()
    {
 
    }

    public virtual void TakeDamage(HZSpell SpellInfo)
    {
        CurHP1 -= SpellInfo.SpellDamage1;
        if(hpSlider)
        hpSlider.value = CurHP1;
    }

    public virtual void HPRegen()
    {
        CurHP1 += HpRegen1;
    }

    public float MaxHPSet { get { return MaxHP; } set { MaxHP = value; hpSlider.maxValue = value; CurHP1 = value; } }

    public void StateByWave()
    {
        Damage += CharactersDB.characterDB.StateUp(Damage);
        MaxHPSet += CharactersDB.characterDB.StateUp(MaxHP);
        MoveSpeed += CharactersDB.characterDB.StateUp(MoveSpeed);
        SpellSDataBase.Attac.SpellDamage1 += CharactersDB.characterDB.StateUp(SpellSDataBase.Attac.SpellDamage1);
    }
}
