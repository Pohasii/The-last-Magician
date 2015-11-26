using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Element
{
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

    Color color;
    public Color Color1
    {
        get { return color; }
        set { color = value; }
    }

    public Element()
    {
    
    }

    public Element(string p_Name, string p_Description, int p_Cost, Vector3 p_StartPos, Material p_Material, Color p_Color)
    {
        Name = p_Name;
        Description = p_Description;
        cost = p_Cost;
        StartPos = p_StartPos;
        material = p_Material;
        color = p_Color;
    }
}
