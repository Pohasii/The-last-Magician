using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Element
{
    int id;
    public int Id
    {
        get { return id; }
    }

    string Name;
    public string Name1
    {
        get { return Name; }
        set { Name = value; }
    }

    string Description;
    public string Description1
    {
        get { return Description; }
        set { Description = value; }
    }

    int cost;
    public int Cost1
    {
        get { return cost; }
        set { cost = value; }
    }

    Vector3 StartPos;
    public Vector3 StartPos1
    {
        get { return StartPos; }
        set { StartPos = value; }
    }

    [SerializeField]
    Material material;
    public Material Material1
    {
        get { return material; }
        set { material = value; }
    }

    Sprite Sprite;
    public Sprite Sprite1
    {
        get { return Sprite; }
        set { Sprite = value; }
    }

    public Element()
    {
    
    }

    public Element(int p_id ,string p_Name, string p_Description, int p_Cost, Vector3 p_StartPos, Material p_Material)
    {
        id = p_id;
        Name = p_Name;
        Description = p_Description;
        cost = p_Cost;
        material = p_Material;
        StartPos = p_StartPos;
        Sprite = Resources.Load<Sprite>("ElementsImage/" + p_Name);
    }

    public Element(string p_Name, string p_Description)
    {
        Name = p_Name;
        Description = p_Description;
    }
}
