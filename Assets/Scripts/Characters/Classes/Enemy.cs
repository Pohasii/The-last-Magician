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

    bool isDead;
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

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

        Target = PlayerScript.playerScript.GetComponent<Transform>();
        EnemyNav = nav;
        anim = p_anim;

        isDead = false;
    }

    public void Move(Transform myTransform)
    {
        if (!PlayerScript.playerScript.player.isDead)
            EnemyNav.SetDestination(Target.position);
        else
            EnemyNav.Stop();
    }

    public void Dead(Transform myTransform, string p_name)
    {
        if (CurHP <= 0)
        {
            if (!IsDead)
            {
                GameObject.Instantiate(ScoreRune, myTransform.position + Vector3.up, Quaternion.identity);
                GameController.RespNewEnemy(p_name);
                anim.SetTrigger("Dead");
                myTransform.GetComponent<EnemyScript>().DisableComponents();
                hpSlider.gameObject.SetActive(false);
                GameObject.Destroy(myTransform.gameObject, 3);
                IsDead = true;
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
        if (PlayerScript.playerScript.player.isDead)
            anim.SetBool("Attac", false);
    }

    public override void Attac()
    {
        timer = 0f;
        if (!PlayerScript.playerScript.player.isDead)
        {
            PlayerScript.playerScript.player.TakeDamageBase(Damage);
        }
    }
}
