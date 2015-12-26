using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptResourceRune : MonoBehaviour
{
    public int i;
    public Element element;
    public static float maxCoolDown;
    public float CurCoolDown;
    Transform myTransform;
    public bool continueCD;
    bool CDStart;

    void Start()
    {
        element = SpellSDataBase.ElementsInDB[i];
        myTransform = GetComponent<Transform>();
        maxCoolDown = 10;
        GetComponent<Image>().sprite = element.Sprite1;
    }

    void Update()
    {
        CoolDown();
        CD();
        if (Input.GetKeyDown(PlayerScript.GodModeSwitch))
        {
            ResetCoolDown();
        }
    }

    public void CD()
    {
        if(continueCD)
        CurCoolDown -= Time.deltaTime;
    }

    public void CoolDown()
    {
        if (CurCoolDown > 0)
        {
            if(CDStart)
            CurCoolDown -= Time.deltaTime;

            myTransform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            myTransform.GetChild(0).GetComponent<Image>().fillAmount = CurCoolDown / maxCoolDown;
        }
        else
        {
            myTransform.GetChild(0).GetComponent<Image>().fillAmount = 1;
            myTransform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            continueCD = false;
        }
    }

    public void StartCD(bool p_CDStart)
    {
        CDStart = p_CDStart;
        CurCoolDown = maxCoolDown;
    }

    public void ContinueCD()
    {
        continueCD = true;
    }

    public void ResetCoolDown()
    {
        CurCoolDown = 0;
        maxCoolDown = 0;
    }
}
