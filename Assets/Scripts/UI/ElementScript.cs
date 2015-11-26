using UnityEngine;
using System.Collections;

public class ElementScript : MonoBehaviour
{
    public Element element;
    public int inSlot;
    public bool InSlot;
    Collider myCollider;
    UIController UICLink;
    RectTransform myTransform;

    void Start()
    {
        myCollider = GetComponent<Collider>();
        UICLink = GetComponentInParent<UIController>();
        myTransform = GetComponent<RectTransform>();
        GetComponent<Renderer>().material = element.Material1;
    }

    void Update()
    {
        if(InSlot)
        myCollider.enabled = !UIController.isDrag;
        UICLink.MoveThis(inSlot, ref myTransform, element.StartPos1);
    }
}
