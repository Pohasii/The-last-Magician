using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Enemy : Character
{
    NavMeshAgent EnemyNav;
    Transform Target;

    float EnemyMP;
    float EnemyMPRegen;

    bool playerInRange;
    public bool PlayerInRange
    {
        get { return playerInRange; }
        set { playerInRange = value; }
    }

    float timer;
    float EnemyAttacDelay;

    PlayerScript playerScripts;

    Animator anim;

    public static GameObject ScoreRune;

    public Enemy(Animator p_anim, NavMeshAgent nav, Canvas p_Canvas, float p_Damage, float p_AttacDelay, float p_HP, float p_MP, float p_HPRegen, float p_MPRegen)
        : base(p_Damage, p_HP, p_HPRegen, 5)
    {
        EnemyAttacDelay = p_AttacDelay;
        EnemyMP = p_MP;

        EnemyMPRegen = p_MPRegen;

        hpSlider = p_Canvas.transform.GetChild(0).GetComponent<Slider>();
        hpSlider.maxValue = MaxHP;
        hpSlider.value = CurHP;

        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerScripts = Target.GetComponent<PlayerScript>();
        EnemyNav = nav;
        anim = p_anim;
    }

    public void Move(Transform myTransform)
    {
        if (!playerScripts.player.isDead)
            EnemyNav.SetDestination(Target.position);
        else
            EnemyNav.Stop();

        if (CurHP <= 0)
        {
            GameObject.Instantiate(ScoreRune, myTransform.position, Quaternion.identity);
            anim.SetTrigger("Dead");
        }
    }

    public void AttacTrigger()
    {
        timer += Time.deltaTime;
        if (timer >= EnemyAttacDelay && playerInRange && !playerScripts.player.isDead)
        {
            Attac();
        }
    }

    public void Attac()
    {
        timer = 0f;
        if (!playerScripts.player.isDead)
        {
            playerScripts.player.TakeDamageBase(Damage);
        }
    }
}
