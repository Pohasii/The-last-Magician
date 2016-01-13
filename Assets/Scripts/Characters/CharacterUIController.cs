using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterUIController : MonoBehaviour
{
    public static CharacterUIController charUIController;

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
                CloseInGameMenu();
        }
    }

    IEnumerator UnPause()
    {
        yield return new WaitForEndOfFrame();
        Time.timeScale = 1;
        InGameMenu.SetActive(false);
        Cursor.visible = false;
        paused = false;
    }

    public void CloseInGameMenu()
    {
        StartCoroutine(UnPause());
    }

    public static void SetText(string MessageText, Color p_Color, int p_FontSize)
    {
        animator.enabled = false;
        TextBar1.color = new Color(p_Color.r, p_Color.g, p_Color.b, 1);
        TextBar1.text = MessageText;
        TextBar1.fontSize = p_FontSize;
    }

    public static void SetTextTrigger(string p_MessageText, Color p_Color, int p_FontSize)
    {
        TextBar1.color = p_Color;
        TextBar1.text = p_MessageText;
        TextBar1.fontSize = p_FontSize;
        animator.SetTrigger("NoRune");
    }

    public static void SetSpellInfo(string p_MessageText, Color p_TextColor, Sprite p_SpellImage)
    {
        TextBar2.color = p_TextColor;
        TextBar2.text = p_MessageText;
        spellImage.sprite = p_SpellImage;
    }
}
