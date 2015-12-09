using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterUIController : MonoBehaviour
{
    static Animator animator;
    static Text MessageText;

    void Start()
    {
        animator = GetComponent<Animator>();
        MessageText = GetComponent<Text>();
    }

    void Update()
    {

    }

    public static void SetTextTrigger(string p_MessageText)
    {
        MessageText.text = p_MessageText;
        animator.SetTrigger("NoRune");
    }
}
