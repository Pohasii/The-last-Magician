using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SpellScript : MonoBehaviour
{
    public Spell spell;
    public int inSlot;
    Collider myCollider;
    UIController UICLink;
    RectTransform myTransform;
    Menu menu;

    void Start()
    {
        myCollider = GetComponent<Collider>();
        UICLink = GetComponentInParent<UIController>();
        myTransform = GetComponent<RectTransform>();
        GetComponent<Renderer>().material = spell.Material1;
    }

    void Update()
    {
        myCollider.enabled = !UIController.isDrag;
        UICLink.MoveThis(inSlot, ref myTransform);
    }
}
