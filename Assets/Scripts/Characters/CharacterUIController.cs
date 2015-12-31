using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterUIController : MonoBehaviour
{
    public static CharacterUIController charUIController;

    GameObject RRuneObj;
    RectTransform Canvas;

    static Animator animator;
    static Text TextBar1, TextBar2;
    static Image spellImage;

    public GameObject InGameMenu;//Меню паузы
    public GameObject HelpWindow;//Помощь

    public static bool paused;

    void Start()
    {
        charUIController = this;

        Canvas = GetComponent<RectTransform>();
        animator = Canvas.FindChild("TextBar1").GetComponent<Animator>();
        TextBar1 = Canvas.FindChild("TextBar1").GetComponent<Text>();
        TextBar2 = Canvas.FindChild("TextBar2").GetComponent<Text>();
        spellImage = Canvas.FindChild("SpellImage").GetComponent<Image>();

        paused = false;
        Cursor.visible = false;
    }

    void SpawnRRune()
    {
        float x = 10;
        float y = -40;

        for (int i = 0; i < 3; i++)
        {
            GameObject fireRune = Instantiate(RRuneObj);
            GameObject frostRune = Instantiate(RRuneObj);
            GameObject ArcaneRune = Instantiate(RRuneObj);

            fireRune.transform.SetParent(Canvas);
            frostRune.transform.SetParent(Canvas);
            ArcaneRune.transform.SetParent(Canvas);

            fireRune.GetComponent<RectTransform>().anchoredPosition = new Vector2(x += 15, y);
            frostRune.GetComponent<RectTransform>().anchoredPosition = new Vector2(x + 47.5f, y);
            ArcaneRune.GetComponent<RectTransform>().anchoredPosition = new Vector2(x + 94.5f, y);

            fireRune.GetComponent<RectTransform>().localScale = Vector3.one;
            frostRune.GetComponent<RectTransform>().localScale = Vector3.one;
            ArcaneRune.GetComponent<RectTransform>().localScale = Vector3.one;

            fireRune.GetComponent<ScriptResourceRune>().element = SpellSDataBase.FireElement;
            frostRune.GetComponent<ScriptResourceRune>().element = SpellSDataBase.FrostElement;
            ArcaneRune.GetComponent<ScriptResourceRune>().element = SpellSDataBase.ArcaneElement;

            PlayerScript.playerScript.RRune.Add(fireRune);
            PlayerScript.playerScript.RRune.Add(frostRune);
            PlayerScript.playerScript.RRune.Add(ArcaneRune);
        }
    }

    void Update()
    {
        Show_CloseInGameMenu();
    }

    public void Show_CloseInGameMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!InGameMenu.activeInHierarchy)
            {
                Time.timeScale = 0;
                InGameMenu.SetActive(true);
                Cursor.visible = true;
                paused = true;
            }
            else
            {
                Time.timeScale = 1;
                InGameMenu.SetActive(false);
                Cursor.visible = false;
                paused = false;
            }
        }
    }

    public void CloseInGameMenu()
    {
        Time.timeScale = 1;
        InGameMenu.SetActive(false);
        Cursor.visible = false;
        paused = false;
    }

    public static void SetText(string MessageText, Color p_Color)
    {
        animator.enabled = false;
        TextBar1.color = new Color(p_Color.r, p_Color.g, p_Color.b, 1);
        TextBar1.text = MessageText;
    }

    public static void SetSpellInfo(string p_MessageText, Color p_TextColor, Sprite p_SpellImage)
    {
        TextBar2.color = p_TextColor;
        TextBar2.text = p_MessageText;
        spellImage.sprite = p_SpellImage;
    }

    public static void SetTextTrigger(string p_MessageText, Color p_Color)
    {
        TextBar1.color = p_Color;
        TextBar1.text = p_MessageText;
        animator.SetTrigger("NoRune");
    }
}
