using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Enemy
{
    NavMeshAgent EnemyNav;
    Transform Target;

    float EnemyDamage;
    float EnemyMaxHP;
    float EnemyCurHp;
    float EnemyMP;
    float EnemyMoveSpeed;
    float EnemyHPRegen;
    float EnemyMPRegen;

    bool playerInRange;
    public bool PlayerInRange
    {
        get { return playerInRange; }
        set { playerInRange = value; }
    }

    float timer;
    float EnemyAttacDelay;
    bool isDead = false;
    public bool IsDead1
    {
        get { return isDead; }
        set { isDead = value; }
    }

    PlayerScript playerScripts;

    Canvas canvas;
    Slider EnemyHPSlider;

    public static GameObject ScoreRune;

    public Enemy(NavMeshAgent nav, Canvas p_Canvas,float Damage, float AttacDelay,float HP, float MP, float HPRegen, float MPRegen)
    {
        EnemyAttacDelay = AttacDelay;

        EnemyDamage = Damage;
        EnemyCurHp = HP;
        EnemyMaxHP = HP;
        EnemyMP = MP;
        EnemyHPRegen = HPRegen;
        EnemyMPRegen = MPRegen;

        canvas = p_Canvas;
        EnemyHPSlider = p_Canvas.transform.GetChild(0).GetComponent<Slider>();
        EnemyHPSlider.maxValue = EnemyMaxHP;
        EnemyHPSlider.value = EnemyCurHp;

        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerScripts = Target.GetComponent<PlayerScript>();
        EnemyNav = nav;
    }

    public void Move(Transform myTransform)
    {
        if(!playerScripts.player.IsDead1)
        EnemyNav.SetDestination(Target.position);
        else
        EnemyNav.Stop();

        EnemyHPSlider.value = EnemyCurHp;
        
        if (EnemyCurHp <= 0)
        {
            GameObject.Instantiate(ScoreRune, myTransform.position, Quaternion.identity);
            isDead = true;
        }
    }

    public void TakeDamage(float Damage)
    {
        EnemyCurHp -= Damage;
    }

    public void AttacTrigger()
    {
        timer += Time.deltaTime;
        if (timer >= EnemyAttacDelay && playerInRange && !playerScripts.player.IsDead1)
        {
            Attac();
        }
    }

    public void Attac()
    {
        timer = 0f;

        if (!playerScripts.player.IsDead1)
        {
            playerScripts.player.TakeDamage(EnemyDamage);
        }
    }
}
