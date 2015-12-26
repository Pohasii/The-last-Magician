using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript playerScript;

    public static Transform myTransform;
    public static Rigidbody myRigidBody;
    public Transform cameraTransform;

    public List<Spell> Spells = new List<Spell>();
    public Transform SpellSpawnPos;

    public Slider HpSlider;
    public Slider CastBar;
    public Image DamageImage;

    public Player player;

    public List<GameObject> RRune = new List<GameObject>();

    int NumOfActiveSpell;

    public delegate void MovePlayer();
    public static event MovePlayer OnPlayerMove;

    void Awake()
    {
        playerScript = this;
        Spell.playerScript = GetComponent<PlayerScript>();

        myTransform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        Spells = GameController.PlayerSpells;
        //Spells = SpellSDataBase.Spells;

        Player.damageImage = DamageImage;
        Player.playerHPSlider = HpSlider;

        player = new Player(Spells, CharactersDB.characterDB.HP, CharactersDB.characterDB.HPRegen, CharactersDB.characterDB.MoveSpeed);
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

    public void RuneCoolDown(Spell spell, bool CDStart)
    {
        for (int i = 0; i < spell.ComponentsOfSpell1.Count; i++)
        {
            for (int k = 0; k < RRune.Count; k++)
            {
                if (!CDStart)
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
        if (Spells.Count > 0 && Spells[NumOfActiveSpell].SpellName1 != null)
        {
            Spell.keyClick = Input.GetKeyDown(KeyCode.R);
            Transform Parametr = null;

            if (Spells[NumOfActiveSpell].SpellName1 == "Blink")
                Parametr = myTransform;
            else
                Parametr = SpellSpawnPos;

            Spells[NumOfActiveSpell].SpellCast(Parametr, SpellSpawnPos);
        }
    }

    void Update()
    {
        SpellChange();

        SpellCast();

        if (Spells.Count > 0)
            CharacterUIController.SetText(Spells[NumOfActiveSpell].SpellName1, Color.red, Spells[NumOfActiveSpell].SpellImage1);

        player.Turning();
        player.DamagedEffect();

        enabled = !player.isDead;
    }

    bool GodMode = false;
    public static KeyCode GodModeSwitch = KeyCode.T;
    void SpellChange()
    {
        if(Input.GetKeyDown(GodModeSwitch))
        {
            if (!GodMode)
            {
                Spells = SpellSDataBase.Spells;
                CharacterUIController.SetTextTrigger("Режим БОГА", Color.red);
                GodMode = true;
            }
            else
            {
                Spells = GameController.PlayerSpells;
                ScriptResourceRune.maxCoolDown = 10;
                NumOfActiveSpell = 0;
                CharacterUIController.SetTextTrigger("Режим не БОГА", Color.red);
                GodMode = false;
            }
        }

        if (NumOfActiveSpell > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            if (OnPlayerMove != null)
                OnPlayerMove();
            if (Spells[NumOfActiveSpell].sp.Count > 0)
                Spells[NumOfActiveSpell].RemoveSpellPoints();
            NumOfActiveSpell--;
        }
        if (NumOfActiveSpell < Spells.Count - 1 && Spells[NumOfActiveSpell + 1].SpellName1 != null && Input.GetKeyDown(KeyCode.E))
        {
            if (OnPlayerMove != null)
                OnPlayerMove();
            if (Spells[NumOfActiveSpell].sp.Count > 0)
                Spells[NumOfActiveSpell].RemoveSpellPoints();
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
        //if (Spells.Count > 0)
            //OnPlayerMove += Spells[NumOfActiveSpell].StopCast;
    }
    void OnDisable()
    {
        //if (Spells.Count > 0)
        //    OnPlayerMove -= Spells[NumOfActiveSpell].StopCast;
    }
}
