using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController controller;

    public static List<Spell> PlayerSpells = new List<Spell>();
    public static int RuneCount = 1200;

    public GameObject ScoreRuneObj;

    void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else
            if (controller != this)
            {
                Destroy(gameObject);
            }
    }
}

public static class SaveClass
{
    public static float PlayerDamage = CharactersDB.characterDB.PlayerDamage;
    public static float PlayerHP = CharactersDB.characterDB.PlayerHP;
    public static float PlayerHPRegen = CharactersDB.characterDB.PlayerHPRegen;
    public static float PlayerArmor = CharactersDB.characterDB.PlayerArmor;
    public static float PlayerMoveSpeed = CharactersDB.characterDB.PlayerMoveSpeed;
    public static SpellResistance PlayerMagResist = CharactersDB.characterDB.PlayerMagResist;


    public static float CreepDamage = CharactersDB.characterDB.CreepDamage;
    public static float CreepAttacDelay = CharactersDB.characterDB.CreepAttacDelay;
    public static float CreepHP = CharactersDB.characterDB.CreepHP;
    public static float CreepArmor = CharactersDB.characterDB.CreepArmor;
    public static float CreepMoveSpeed = CharactersDB.characterDB.CreepMoveSpeed;
    public static SpellResistance CreepMagResist = CharactersDB.characterDB.CreepMagResist;


    public static float BossDamage = CharactersDB.characterDB.BossDamage;
    public static float BossAttacDelay = CharactersDB.characterDB.BossAttacDelay;
    public static float BossHP = CharactersDB.characterDB.BossHP;
    public static float BossArmor = CharactersDB.characterDB.BossArmor;
    public static float BossMoveSpeed = CharactersDB.characterDB.BossMoveSpeed;
    public static SpellResistance BossMagResist = CharactersDB.characterDB.BossMagResist;

    public static int maxEnemyCount = EnemySpawn.enemySpawn.maxEnemyCount;
    public static int startEnemyCount = EnemySpawn.enemySpawn.startEnemyCount;
    public static int burstEnemyCount = EnemySpawn.enemySpawn.burstEnemyCount;


}