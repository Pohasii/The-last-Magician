using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Enemy : Character
{
    public Animator anim;
    public NavMeshAgent EnemyNav;
    Transform Target;

    bool playerInRange;
    public bool PlayerInRange
    {
        get { return playerInRange; }
        set { playerInRange = value; }
    }

    float timer;
    float EnemyAttacDelay;

    bool isDead;
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public Enemy(Transform p_Transform, float p_Damage, float p_AttacDelay, float p_HP, float p_MoveSpeed)
        : base(p_Damage, p_HP, p_MoveSpeed)
    {
        anim = p_Transform.GetComponent<Animator>();
        EnemyNav = p_Transform.GetComponent<NavMeshAgent>();

        hpSlider = p_Transform.GetChild(0).GetComponentInChildren<Slider>();
        hpSlider.maxValue = MaxHP;
        hpSlider.value = CurHP1;

        Target = PlayerScript.playerScript.GetComponent<Transform>();

        EnemyAttacDelay = p_AttacDelay;
        EnemyNav.speed = p_MoveSpeed;

        isDead = false;
    }

    public void Move()
    {
        if (!PlayerScript.playerScript.player.isDead)
            EnemyNav.SetDestination(Target.position);
        else
            EnemyNav.Stop();
    }

    public void Dead(Transform myTransform)
    {
        if (CurHP1 <= 0)
        {
            if (!IsDead)
            {
                myTransform.GetComponent<EnemyScript>().DisableComponents();
            }
        }
    }

    public void AttacTrigger()
    {
        timer += Time.deltaTime;
        if (timer >= EnemyAttacDelay && playerInRange && !PlayerScript.playerScript.player.isDead)
        {
            Attac();
        }
    }

    public override void Attac()
    {
        timer = 0f;
        if (!PlayerScript.playerScript.player.isDead)
        {
            PlayerScript.playerScript.player.TakeDamageBase(Damage);
            anim.Play("attack");
        }
    }
}
