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

    bool CheckElement()//является ли этот елемент елементом одного массивов FireELementCount, FrostElementCount или ArcaneElementCount
    {
        foreach (ElementScript eS in Menu.SMenu.FireELementCount)
        {
            if (eS == this)
                return true;
        }
        foreach (ElementScript eS in Menu.SMenu.FrostElementCount)
        {
            if (eS == this)
                return true;
        }
        foreach (ElementScript eS in Menu.SMenu.ArcaneElementCount)
        {
            if (eS == this)
                return true;
        }
        return false;
    }

    void ELementCount()//добовляем елементы в соответствующие им массивы для посчета количества каждого из елементов
    {
        if (!InSlot && !CheckElement())//если елеметн не входит в одни из этих массивов и он не находится в слоте, то добовляем его в соответсвующий массив
        {
            if (element.Id == 0)
                Menu.SMenu.FireELementCount.Add(this);
            if (element.Id == 1)
                Menu.SMenu.FrostElementCount.Add(this);
            if (element.Id == 2)
                Menu.SMenu.ArcaneElementCount.Add(this);
        }
        else
            if (InSlot && CheckElement())//если елемент въодит в один из массивов и находится слоте, то удаляем его из массива 
            {
                if (element.Id == 0)
                    Menu.SMenu.FireELementCount.Remove(this);
                if (element.Id == 1)
                    Menu.SMenu.FrostElementCount.Remove(this);
                if (element.Id == 2)
                    Menu.SMenu.ArcaneElementCount.Remove(this);
            }
    }

    void Update()
    {
        ELementCount();

        if (InSlot)
            myCollider.enabled = !UIController.isDrag;
        UICLink.MoveThisElement(inSlot, ref myTransform, element.StartPos1);
    }
}
