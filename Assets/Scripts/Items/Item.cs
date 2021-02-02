using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GradeType { Common, Uncommon, Rare, Epic, Legendary, Unique}    

public abstract class Item<T>
{
    public int ID { get; protected set; }
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public string Slug { get; protected set; }
    public GradeType Grade { get; protected set; }
    public Sprite Icon { get; protected set; }
    public Color Color { get; protected set; }

    public abstract T Test(T frame);
    
    public Item()
    {
        ID = -1;
    }

    protected Item(int id, string title, string desc, string slug)
    {
        ID = id;
        Title = title;
        Description = desc;
        Slug = slug;

        Icon = Resources.Load<Sprite>(Slug);
    }
    protected Item(int id, string title, string desc, string slug, GradeType grade)
    {
        ID = id;
        Title = title;
        Description = desc;
        Slug = slug;
        Grade = grade;

        //Sprite TBD
    }

    public void SelectGrade(GradeType gd)
    {
        Grade = gd;
        switch (Grade)
        {
            case GradeType.Common:
                Color = Color.gray;
                break;
            case GradeType.Uncommon:
                Color = Color.green;
                break;
            case GradeType.Rare:
                Color = Color.blue;
                break;
            case GradeType.Epic:
                Color = Color.blue + Color.red;
                break;
            case GradeType.Legendary:
                Color = Color.red + Color.yellow;
                break;
            case GradeType.Unique:
                break;
            default:
                break;
        }
    }
}
