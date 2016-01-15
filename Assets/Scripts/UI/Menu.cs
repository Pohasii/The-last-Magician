using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    public static Menu SMenu;

    public GameObject BackGround, SlotForSpells;//задний фон и префаб слота для спелов

    public static List<Spell> SpellInSlot = new List<Spell>();//спелы в слоте
    public static List<GameObject> SlotsForSpells = new List<GameObject>();//слоты для спелов
    public static List<GameObject> SpellsObjInSlot = new List<GameObject>();//объекты спелов в слоте

    public List<Element> ElementsInSlots = new List<Element>();//елементы в слоте
    public List<GameObject> SlotsForElements = new List<GameObject>();//слоты для елементов
    public List<GameObject> ElementsObjInSlot = new List<GameObject>();//объекты елементов в слоте

    public GameObject UISpellPrefab, UIElementPrefab;//префабы спела и елемента для интерфейса

    public RectTransform FireElementPos, FrostElementPos, ArcaneElementPos;//стартовые позиции елементов
    public Text[] ElementCountText;//Текст для отображения количества елементов
    [HideInInspector]
    public List<ElementScript> FireELementCount, FrostElementCount, ArcaneElementCount = new List<ElementScript>();

    public List<Text> StatsText = new List<Text>();//компонент Text для вывода характеристик персонажа

    [SerializeField]
    private GameObject ToolTip;//префаб всплывающей подсказки

    [SerializeField]
    private Text RuneCountText;//компонент Text для вывода количества рун
    [SerializeField]
    private Animator animator;//компонент Animator для анимации всплывающего сообщения

    void Awake()
    {
        SMenu = this;
        Debug.Log(SpellsObjInSlot.Count);
    }

    void Start()
    {
        BackGround = GameObject.FindGameObjectWithTag("MenuBackGround");
        BackGround.transform.SetAsFirstSibling();
        CharactersDB.characterDB.SetStatsText(StatsText);
    }

    void Update()
    {
        ElementCountText[0].text = FireELementCount.Count.ToString();
        ElementCountText[1].text = FrostElementCount.Count.ToString();
        ElementCountText[2].text = ArcaneElementCount.Count.ToString();

        RuneCountText.text = GameController.RuneCount.ToString();
    }

    public void ShowToolTip(Vector3 Position, Element element) //Для компонентов
    {
        ToolTip.SetActive(true);
        ToolTip.GetComponent<RectTransform>().localPosition = new Vector3(Position.x + 10, Position.y - 10, -31);
        ToolTip.transform.GetChild(0).GetComponent<Text>().text = element.Name1;
        ToolTip.transform.GetChild(1).GetComponent<Text>().text = element.Description1;
        ToolTip.transform.SetAsLastSibling();
    }

    public void ShowToolTip(Vector3 Position, Spell spell) // Для спелов
    {
        ToolTip.SetActive(true);
        ToolTip.GetComponent<RectTransform>().localPosition = new Vector3(Position.x + 25, Position.y - 25, -31);
        ToolTip.transform.GetChild(0).GetComponent<Text>().text = spell.SpellName1;
        ToolTip.transform.GetChild(1).GetComponent<Text>().text = spell.SpellDescription1;
        ToolTip.transform.SetAsLastSibling();
    }

    public void CloseToolTip()
    {
        ToolTip.SetActive(false);
    }

    public void ElementCheck()
    {
        bool IsSpell = false;
        for (int i = 0; i < SpellSDataBase.Spells.Count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (SpellSDataBase.Spells[i].ComponentsOfSpell1[j] != ElementsInSlots[j])
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
                CreateSpell(SpellSDataBase.Spells[i]);
                for (int k = 0; k < ElementsInSlots.Count; k++)
                {
                    ElementsInSlots[k] = new Element();
                    Destroy(ElementsObjInSlot[k]);
                }
            }
        }
    }

    public void CreateSlotForSpells()
    {
        SpellInSlot.Add(new Spell());
        //if (SpellInSlot[0].SpellName1 == null)
        //    return;
        GameObject slot = Instantiate(SlotForSpells);
        slot.transform.SetParent(SlotForSpells.transform.parent);
        slot.GetComponent<RectTransform>().localScale = SlotForSpells.transform.localScale;
        slot.GetComponent<RectTransform>().localPosition = SlotForSpells.GetComponent<RectTransform>().localPosition + Vector3.right * SlotForSpells.GetComponent<RectTransform>().localScale.x * SlotsForSpells.Count;
        slot.GetComponent<Slot>().CurSlot = SlotsForSpells.Count;
        SlotsForSpells.Add(slot);
        SpellsObjInSlot.Add(null);
        if (SpellInSlot.Count < 1)
            return;
        slot.transform.parent.GetComponent<RectTransform>().sizeDelta += Vector2.right * SlotForSpells.GetComponent<RectTransform>().localScale.x;
    }

    public void CreateSpell(Spell CreatedSpell)
    {
        CreateSlotForSpells();
        for (int i = 0; i < SpellInSlot.Count; i++)
        {
            if (SpellInSlot[i].SpellName1 == null)
            {
                GameObject ob = (GameObject)Instantiate(UISpellPrefab, Vector3.down * 5, UISpellPrefab.transform.rotation);
                SpellInSlot[i] = CreatedSpell;
                SpellsObjInSlot[i] = ob;
                ob.transform.SetParent(GetComponent<RectTransform>());
                ob.GetComponent<SpellScript>().spell = CreatedSpell;
                ob.GetComponent<SpellScript>().inSlot = i;
                ob.GetComponent<RectTransform>().localScale = UISpellPrefab.transform.localScale;
                break;
            }
        }
    }

    public void ElementSpawn(Element p_element)
    {
        if (GameController.RuneCount >= p_element.Cost1)
        {
            GameObject ob = (GameObject)Instantiate(UIElementPrefab, Vector3.up * 10, Quaternion.identity);
            GameController.RuneCount -= p_element.Cost1;
            ob.transform.SetParent(GetComponent<RectTransform>());
            ob.GetComponent<ElementScript>().element = p_element;
            ob.GetComponent<RectTransform>().localScale = UIElementPrefab.transform.localScale;
        }
        else
        {
            animator.SetTrigger("NoRune");
        }
    }

    public void ElementReshuffle(int ElementSloTNum, GameObject elementObj, bool OnceClick)
    {
        ElementScript tempEScript = elementObj.GetComponent<ElementScript>();
        if (OnceClick)
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

    public void LoadLevel()
    {
        GameController.PlayerSpells = SpellInSlot;
    }
}
