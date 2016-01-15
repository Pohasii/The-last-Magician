using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Character
{
    private float Damage;
    public float Damage1
    {
        get { return Damage; }
        set { Damage = value; }
    }

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

    [SerializeField]
    float Armor;
    public float Armor1
    {
        get { return Armor; }
        set { Armor = value; }
    }

    private float MoveSpeed;
    public float MoveSpeed1
    {
        get { return MoveSpeed; }
        set { MoveSpeed = value; }
    }

    public SpellResistance MagResist;

    public Slider hpSlider;

    public Character(float p_Dmg, float p_MaxHP, float p_HPRegen, float p_Armor, float p_MoveSpeed, SpellResistance p_MagResist)
    {
        Damage = p_Dmg;
        MaxHP = p_MaxHP;
        CurHP = p_MaxHP;
        HpRegen = p_HPRegen;
        Armor = p_Armor;
        MoveSpeed = p_MoveSpeed;
        MagResist = p_MagResist;
    }

    public Character(float p_Dmg, float p_MaxHP, float p_Armor, float p_MoveSpeed, SpellResistance p_MagResist)
    {
        Damage = p_Dmg;
        MaxHP = p_MaxHP;
        CurHP = p_MaxHP;
        Armor = p_Armor;
        MoveSpeed = p_MoveSpeed;

        SpellResistance[] SpellRes = new SpellResistance[6];

        SpellRes[0] = new SpellResistance(SpellDamageType.Fire, Random.Range(8, 12));
        SpellRes[1] = new SpellResistance(SpellDamageType.Frost, Random.Range(8, 12));
        SpellRes[2] = new SpellResistance(SpellDamageType.Arcane, Random.Range(8, 12));
        SpellRes[3] = new SpellResistance(SpellDamageType.Holy, Random.Range(8, 12));
        SpellRes[4] = new SpellResistance(SpellDamageType.Nature, Random.Range(8, 12));
        SpellRes[5] = new SpellResistance(SpellDamageType.Shadow, Random.Range(8, 12));

        MagResist = SpellRes[Random.Range(0, SpellRes.Length)];

    }

    public virtual void Attac()
    {

    }

    public virtual void TakeDamage(HZSpell SpellInfo)
    {
        CurHP1 -= MagResist.ResistanceCanculate(SpellInfo.SpellDamageType, SpellInfo.SpellDamage1);
    }

    public virtual void HPRegen()
    {
        CurHP1 += HpRegen1;
    }

    public float MaxHPSet { get { return MaxHP; } set { MaxHP = value; hpSlider.maxValue = value; CurHP1 = value; } }

    public float DamageCanculate(float Damage)
    {
        float NewDamage;
        NewDamage = Damage - ((Armor * 1.6180339887f) / 100f) * 10f;
        return NewDamage;
    }

    public void StateByWave()
    {
        Damage1 += CharactersDB.characterDB.StateUp(Damage1);
        MaxHPSet += CharactersDB.characterDB.StateUp(MaxHP) / 4;
        MoveSpeed1 += CharactersDB.characterDB.StateUp(MoveSpeed1) / 8;
        SpellSDataBase.Attac.SpellDamage1 += CharactersDB.characterDB.StateUp(SpellSDataBase.Attac.SpellDamage1);
    }
}

[System.Serializable]
public struct SpellResistance
{
    public SpellDamageType ResistanceType;
    public float Resistance;

    public SpellResistance(SpellDamageType p_ResistanceType, float p_Resistance)
    {
        ResistanceType = p_ResistanceType;
        Resistance = p_Resistance;
    }

    public float ResistanceCanculate(SpellDamageType p_ResistanceType, float Damage)
    {
        if (ResistanceType == p_ResistanceType)
        {
            float NewDamage = Damage - Resistance;

            return NewDamage >= 0 ? NewDamage : 0;
        }
        return Damage;
    }
}