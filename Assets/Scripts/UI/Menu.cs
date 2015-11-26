using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject BackGround;

    public List<Spell> SpellInSlot = new List<Spell>();
    public List<GameObject> SlotsForSpells = new List<GameObject>();
    public List<GameObject> SpellsObjInSlot = new List<GameObject>();

    public List<Element> ElementsInSlots = new List<Element>();
    public List<GameObject> SlotsForElements = new List<GameObject>();
    public List<GameObject> ElementsObjInSlot = new List<GameObject>();

    [SerializeField]
    private GameObject ToolTip;

    [SerializeField]
    private Text RuneCountText;
    [SerializeField]
    private Animator animator;

    void Start()
    {
        BackGround = GameObject.FindGameObjectWithTag("MenuBackGround");
        BackGround.transform.SetAsFirstSibling();
    }

    void Update()
    {
        RuneCountText.text = GameController.RuneCount.ToString();
    }

    public void ShowToolTip(Vector3 Position, Element element) //Для компонентов
    {
        ToolTip.SetActive(true);
        ToolTip.GetComponent<RectTransform>().anchoredPosition = new Vector3(Position.x + 10, Position.y - 10, -30);
        ToolTip.transform.GetChild(0).GetComponent<Text>().text = element.Name1;
        ToolTip.transform.GetChild(1).GetComponent<Text>().text = element.Description1;
        ToolTip.transform.SetAsLastSibling();
    }

    public void ShowToolTip(Vector3 Position, Spell spell) // Для спелов
    {
        ToolTip.SetActive(true);
        ToolTip.GetComponent<RectTransform>().anchoredPosition = new Vector3(Position.x + 25, Position.y - 25, Position.z);
        ToolTip.transform.GetChild(0).GetComponent<Text>().text = spell.SpellName1;
        ToolTip.transform.GetChild(1).GetComponent<Text>().text = spell.SpellDescription1;
        ToolTip.transform.SetAsLastSibling();
    }

    public void CloseToolTip()
    {
        ToolTip.SetActive(false);
    }

    public void ElementCheck(List<Spell> spell, GameObject SpellObj)
    {
        bool IsSpell = false;
        for (int i = 0; i < spell.Count; i++)
        {
            for (int j = 0; j < spell.Count; j++)
            {
                if (spell[i].ComponentsOfSpell1[j] != ElementsInSlots[j])
                {
                    IsSpell = false;
                    break;
                }
                else
                {
                    IsSpell = true;
                }
            }
            if (IsSpell)
            {
                CreateSpell(spell[i], SpellObj);
                for (int k = 0; k < ElementsInSlots.Count; k++)
                {
                    ElementsInSlots[k] = new Element();
                    Destroy(ElementsObjInSlot[k]);
                }
            }
        }
    }

    public void CreateSpell(Spell CreatedSpell, GameObject SpellObj)
    {
        for (int i = 0; i < SpellInSlot.Count; i++)
        {
            if (SpellInSlot[i].SpellName1 == null)
            {
                GameObject ob = (GameObject)Instantiate(SpellObj, Vector3.down * 5, Quaternion.identity);
                SpellInSlot[i] = CreatedSpell;
                SpellsObjInSlot[i] = ob;
                ob.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>());
                ob.GetComponent<SpellScript>().spell = CreatedSpell;
                ob.GetComponent<SpellScript>().inSlot = i;
                ob.GetComponent<RectTransform>().localScale = new Vector3(30, 30, 15);
                break;
            }
        }
    }

    public void ElementSpawn(GameObject ElementObj, Element p_element)
    {
        if (GameController.RuneCount >= p_element.Cost1)
        {
            GameObject ob = (GameObject)Instantiate(ElementObj, Vector3.up * 10, Quaternion.identity);
            GameController.RuneCount -= p_element.Cost1;
            ob.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>());
            ob.GetComponent<ElementScript>().element = p_element;
            ob.GetComponent<RectTransform>().localScale = new Vector3(30, 30, 15);
        }
        else
        {
            animator.SetTrigger("NoRune");
        }
    }

    public void ElementRepick(int ElementSloTNum, GameObject elementObj, bool hz)
    {
        ElementScript tempEScript = elementObj.GetComponent<ElementScript>();

        if (hz)
        {
            for (int i = 0; i < ElementsInSlots.Count; i++)
            {
                if (ElementsInSlots[i].Name1 == null)
                {
                    tempEScript.inSlot = i;
                    ElementSloTNum = i;
                    break;
                }
                else
                {
                    if (i == ElementsInSlots.Count - 1)
                    {
                        return;
                    }
                }
            }
        }
        else
        {
            if (ElementsInSlots[ElementSloTNum].Name1 != null)
            {
                if (tempEScript.InSlot)
                {
                    ElementsObjInSlot[ElementSloTNum].GetComponent<ElementScript>().inSlot = tempEScript.inSlot;

                    ElementsObjInSlot[tempEScript.inSlot] = ElementsObjInSlot[ElementSloTNum];
                    ElementsInSlots[tempEScript.inSlot] = ElementsInSlots[ElementSloTNum];
                }
            }
        }

        ElementsInSlots[ElementSloTNum] = tempEScript.element;
        ElementsObjInSlot[ElementSloTNum] = elementObj;

        tempEScript.inSlot = ElementSloTNum;
        tempEScript.InSlot = true;
    }

    public void SpellReshuffle(int SpellSlotNum, GameObject spellObj)
    {
        SpellScript tempSpellScript = spellObj.GetComponent<SpellScript>();

        if (SpellInSlot[SpellSlotNum].SpellName1 != null)
        {
            SpellsObjInSlot[SpellSlotNum].GetComponent<SpellScript>().inSlot = tempSpellScript.inSlot;

            SpellInSlot[tempSpellScript.inSlot] = SpellInSlot[SpellSlotNum];
            SpellsObjInSlot[tempSpellScript.inSlot] = SpellsObjInSlot[SpellSlotNum];
        }

        SpellInSlot[SpellSlotNum] = tempSpellScript.spell;
        SpellsObjInSlot[SpellSlotNum] = spellObj;

        tempSpellScript.inSlot = SpellSlotNum;
    }

    public bool OneClick = false;
    float timer = 0;

    public bool DoubleClick()
    {
        float delay = 0.5f;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (OneClick)
            {
                if ((Time.time - timer) <= delay)
                {
                    OneClick = false;
                    return true;
                }
                else
                {
                    OneClick = false;
                    return false;
                }
            }
            else
            {
                OneClick = true;
                timer = Time.time;
            }
        }
        return false;
    }

    public void LoadLevel()
    {
        GameController.PlayerSpells = SpellInSlot;
        Application.LoadLevel("MainGameScene");
    }
}
