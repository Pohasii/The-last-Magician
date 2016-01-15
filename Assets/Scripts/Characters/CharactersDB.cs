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
    public float PlayerArmor;
    public float PlayerMoveSpeed;
    public SpellResistance PlayerMagResist;

    [Header("Creep stats")]
    public float CreepDamage;
    public float CreepAttacDelay;
    public float CreepHP;
    public float CreepArmor;
    public float CreepMoveSpeed;
    public SpellResistance CreepMagResist;

    [Header("Boss stats")]
    public float BossDamage;
    public float BossAttacDelay;
    public float BossHP;
    public float BossArmor;
    public float BossMoveSpeed;
    public SpellResistance BossMagResist;

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
        boss = new Enemy(p_T, BossDamage, BossAttacDelay, BossHP, BossArmor, BossMoveSpeed, BossMagResist);
    }

    public void SetCreepConstructor(Transform p_T)
    {
        creep = new Enemy(p_T, CreepDamage, CreepAttacDelay, CreepHP, CreepArmor, CreepMoveSpeed, CreepMagResist);
    }

    public void StateByWave()
    {
        CreepDamage += StateUp(CreepDamage);
        CreepHP += StateUp(CreepHP);
        CreepMoveSpeed += StateUp(CreepMoveSpeed) / 4;

        BossDamage += StateUp(BossDamage);
        BossHP += StateUp(BossHP);
        BossMoveSpeed += StateUp(BossMoveSpeed) / 4;
    }

    public float StateUp(float State)
    {
        return State * 1.6180339887f / 100;
    }

    public void Save()
    {
        SaveClass.PlayerDamage = PlayerDamage;
        SaveClass.PlayerHP = PlayerHP;
        SaveClass.PlayerHPRegen = PlayerHPRegen;
        SaveClass.PlayerArmor = PlayerArmor;
        SaveClass.PlayerMoveSpeed = PlayerMoveSpeed;
        SaveClass.PlayerMagResist = PlayerMagResist;

        SaveClass.CreepDamage = CreepDamage;
        SaveClass.CreepHP = CreepHP;
        SaveClass.CreepAttacDelay = CreepAttacDelay;
        SaveClass.CreepArmor = CreepArmor;
        SaveClass.CreepMoveSpeed = CreepMoveSpeed;
        SaveClass.CreepMagResist = CreepMagResist;

        SaveClass.BossDamage = BossDamage;
        SaveClass.BossAttacDelay = BossAttacDelay;
        SaveClass.BossHP = BossHP;
        SaveClass.BossArmor = BossArmor;
        SaveClass.BossMoveSpeed = BossMoveSpeed;
        SaveClass.BossMagResist = BossMagResist;

        SaveClass.maxEnemyCount = EnemySpawn.enemySpawn.maxEnemyCount;
        SaveClass.startEnemyCount = EnemySpawn.enemySpawn.startEnemyCount;
        SaveClass.burstEnemyCount = EnemySpawn.enemySpawn.burstEnemyCount;
        
        Debug.Log(SaveClass.CreepDamage + "Save");
    }

    public void Load()
    {
        PlayerDamage = SaveClass.PlayerDamage;
        PlayerHP = SaveClass.PlayerHP;
        PlayerHPRegen = SaveClass.PlayerHPRegen;
        PlayerArmor = SaveClass.PlayerArmor;
        PlayerMoveSpeed = SaveClass.PlayerMoveSpeed;
        PlayerMagResist = SaveClass.PlayerMagResist;

        CreepDamage = SaveClass.CreepDamage;
        CreepHP = SaveClass.CreepHP;
        CreepAttacDelay = SaveClass.CreepAttacDelay;
        CreepArmor = SaveClass.CreepArmor;
        CreepMoveSpeed = SaveClass.CreepMoveSpeed;
        CreepMagResist = SaveClass.CreepMagResist;

        BossDamage = SaveClass.BossDamage;
        BossAttacDelay = SaveClass.BossAttacDelay;
        BossHP = SaveClass.BossHP;
        BossArmor = SaveClass.BossArmor;
        BossMoveSpeed = SaveClass.BossMoveSpeed;
        BossMagResist = SaveClass.BossMagResist;

        EnemySpawn.enemySpawn.maxEnemyCount = SaveClass.maxEnemyCount;
        EnemySpawn.enemySpawn.startEnemyCount = SaveClass.startEnemyCount;
        EnemySpawn.enemySpawn.burstEnemyCount = SaveClass.burstEnemyCount;

        Debug.Log(SaveClass.CreepDamage + "Load");
    }
}
