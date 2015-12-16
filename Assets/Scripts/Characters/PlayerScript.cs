using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
    Transform myTransform;
    Transform cameraTransform;
    Rigidbody myRigidBody;

    public float HP;
    public float MoveSpeed;
    public float HPRegen;

    public List<Spell> Spells;
    public Transform SpellSpawnPos;

    public Slider HpSlider;
    public Slider CastBar;
    public Image DamageImage;

    Text ScoreRune;

    public Player player;

    public GameObject RRuneObj;
    public List<GameObject> RRune = new List<GameObject>();

    int NumOfActiveSpell;

    public delegate void MovePlayer();
    public static event MovePlayer OnPlayerMove;

    void Awake()
    {
        Spell.playerScript = GetComponent<PlayerScript>();
        myTransform = GetComponent<Transform>();
        cameraTransform = Camera.main.transform;
        myRigidBody = GetComponent<Rigidbody>();
        Spells = GameController.PlayerSpells;
        ScoreRune = GameObject.Find("RuneScore").GetComponent<Text>();
        //Spells = SpellSDataBase.Spells;

        Player.damageImage = DamageImage;
        Player.playerHPSlider = HpSlider;

        player = new Player(myTransform, myRigidBody, Spells, HP, MoveSpeed, HPRegen);

        float x = 10;
        float y = -40;

        for (int i = 0; i < 3; i++)
        {
            GameObject fireRune = Instantiate(RRuneObj);
            GameObject frostRune = Instantiate(RRuneObj);
            GameObject ArcaneRune = Instantiate(RRuneObj);

            fireRune.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>());
            frostRune.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>());
            ArcaneRune.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>());

            fireRune.GetComponent<RectTransform>().anchoredPosition = new Vector2(x += 15, y);
            frostRune.GetComponent<RectTransform>().anchoredPosition = new Vector2(x + 47.5f, y);
            ArcaneRune.GetComponent<RectTransform>().anchoredPosition = new Vector2(x + 94.5f, y);

            fireRune.GetComponent<RectTransform>().localScale = Vector3.one;
            frostRune.GetComponent<RectTransform>().localScale = Vector3.one;
            ArcaneRune.GetComponent<RectTransform>().localScale = Vector3.one;

            fireRune.GetComponent<ScriptResourceRune>().element = SpellSDataBase.FireElement;
            frostRune.GetComponent<ScriptResourceRune>().element = SpellSDataBase.FrostElement;
            ArcaneRune.GetComponent<ScriptResourceRune>().element = SpellSDataBase.ArcaneElement;

            RRune.Add(fireRune);
            RRune.Add(frostRune);
            RRune.Add(ArcaneRune);
        }
    }

    public bool CheckRuneCD(Spell spell)
    {
        bool isSuitable = true;
        bool isTested = true;
        List<GameObject> hz = new List<GameObject>();
        for (int i = 0; i < spell.ComponentsOfSpell1.Count; i++)
        {
            for (int k = 0; k < RRune.Count; k++)
            {
                foreach (GameObject ob in hz)
                {
                    if (RRune[k] == ob)
                    {
                        isTested = false;
                        break;
                    }
                    else
                        isTested = true;
                }
                if (spell.ComponentsOfSpell1[i] == RRune[k].GetComponent<ScriptResourceRune>().element && RRune[k].GetComponent<ScriptResourceRune>().CurCoolDown <= 0 && isTested)
                {
                    isSuitable = true;
                    hz.Add(RRune[k]);
                    break;
                }
                else
                    isSuitable = false;
            }
            if (!isSuitable)
                return false;
        }
        return isSuitable;
    }

    public void RuneCoolDown(Spell spell, bool hzCD)
    {
        for (int i = 0; i < spell.ComponentsOfSpell1.Count; i++)
        {
            for (int k = 0; k < RRune.Count; k++)
            {
                if (!hzCD)
                {
                    if (spell.ComponentsOfSpell1[i] == RRune[k].GetComponent<ScriptResourceRune>().element && RRune[k].GetComponent<ScriptResourceRune>().CurCoolDown <= 0)
                    {
                        RRune[k].GetComponent<ScriptResourceRune>().StartCD(false);
                        break;
                    }
                }
                else
                {
                    if (RRune[k].GetComponent<ScriptResourceRune>().CurCoolDown > 0)
                    {
                        RRune[k].GetComponent<ScriptResourceRune>().ContinueCD();
                    }
                }
            }
        }
    }

    public void RuneCoolDown(Spell spell)
    {
        for (int i = 0; i < spell.ComponentsOfSpell1.Count; i++)
        {
            for (int k = 0; k < RRune.Count; k++)
            {
                if (spell.ComponentsOfSpell1[i] == RRune[k].GetComponent<ScriptResourceRune>().element && RRune[k].GetComponent<ScriptResourceRune>().CurCoolDown <= 0)
                {
                    RRune[k].GetComponent<ScriptResourceRune>().StartCD(true);
                    break;
                }
            }
        }
    }

    void SpellCast()
    {
        if (Spells.Count > 0)
        {
            Spell.keyClick = Input.GetKeyDown(KeyCode.R);
            Transform Parametr = null;

            if (Spells[NumOfActiveSpell].SpellName1 == "Blink")
                Parametr = myTransform;
            else
                Parametr = SpellSpawnPos;

            Spells[NumOfActiveSpell].SpellCast(Parametr, cameraTransform);
        }
        //////////ДЛЯ ТЕСТИРОВАНИЯ
        Spell.keyClick = Input.GetKeyDown(KeyCode.R);
        Transform Parametr1 = null;

        if (SpellSDataBase.Spells[NumOfActiveSpell].SpellName1 == "Blink")
            Parametr1 = myTransform;
        else
            Parametr1 = SpellSpawnPos;

        SpellSDataBase.Spells[NumOfActiveSpell].SpellCast(Parametr1, SpellSpawnPos);
        ///////////////////////
    }

    void Update()
    {
        //SpellChange(Spells);
        SpellChange(SpellSDataBase.Spells);///Для тестирования

        SpellCast();

        ScoreRune.text = NumOfActiveSpell.ToString();//GameController.RuneCount.ToString();

        player.Turning();
        player.DamagedEffect();

        enabled = !player.isDead;
    }

    void SpellChange(List<Spell> p_spell)
    {
        if (NumOfActiveSpell > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            if (OnPlayerMove != null)
                OnPlayerMove();
            if (p_spell[NumOfActiveSpell].sp.Count > 0)
                p_spell[NumOfActiveSpell].RemoveSpellPoints();
            NumOfActiveSpell--;
        }
        if (NumOfActiveSpell < p_spell.Count - 1 && Input.GetKeyDown(KeyCode.E))
        {
            if (OnPlayerMove != null)
                OnPlayerMove();
            if (p_spell[NumOfActiveSpell].sp.Count > 0)
                p_spell[NumOfActiveSpell].RemoveSpellPoints();
            NumOfActiveSpell++;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0)
        {
            if (OnPlayerMove != null)
                OnPlayerMove();
        }
        player.Move(h, v);
    }

    void OnEnable()
    {
        OnPlayerMove += SpellSDataBase.Spells[NumOfActiveSpell].StopCast;///Для тестирования
        if (Spells.Count > 0)
            OnPlayerMove += Spells[NumOfActiveSpell].StopCast;
    }
    void OnDisable()
    {
        OnPlayerMove -= SpellSDataBase.Spells[NumOfActiveSpell].StopCast;///Для тестирования
        if (Spells.Count > 0)
            OnPlayerMove -= Spells[NumOfActiveSpell].StopCast;
    }
}
