using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharactersDB : MonoBehaviour
{
    public static CharactersDB characterDB;

    [Header("Player stats")]
    public float PlayerDamage;
    public float PlayerHP;
    public float PlayerHPRegen;
    public float PlayerMoveSpeed;

    [Header("Creep stats")]
    public float CreepDamage;
    public float CreepAttacDelay;
    public float CreepHP;
    public float CreepMoveSpeed;

    [Header("Boss stats")]
    public float BossDamage;
    public float BossAttacDelay;
    public float BossHP;
    public float BossMoveSpeed;

    public Enemy creep, boss;

    void Awake()
    {
        characterDB = this;
    }

    public void SetStatsText(List<Text> Stats)
    {
        Stats[0].text = "PlayerDamage: " + PlayerDamage.ToString();
        Stats[1].text = "Health: " + PlayerHP.ToString();
        Stats[2].text = "PlayerHP/S: " + PlayerHPRegen.ToString();
        Stats[3].text = "Move speed: " + PlayerMoveSpeed.ToString();
    }

    public void SetBossConstructor(Transform p_T)
    {
        boss = new Enemy(p_T, BossDamage, BossAttacDelay, BossHP, BossMoveSpeed);
    }

    public void SetCreepConstructor(Transform p_T)
    {
        creep = new Enemy(p_T, CreepDamage, CreepAttacDelay, CreepHP, CreepMoveSpeed);
    }

    public void StateByWave()
    {
        CreepDamage += StateUp(CreepDamage);
        CreepHP += StateUp(CreepHP);
        CreepMoveSpeed += StateUp(CreepMoveSpeed);

        BossDamage += StateUp(BossDamage);
        BossHP += StateUp(BossHP);
        BossMoveSpeed += StateUp(BossMoveSpeed);
    }

    public float StateUp(float State)
    {
        return State * 1.6180339887f / 100;
    }
}
